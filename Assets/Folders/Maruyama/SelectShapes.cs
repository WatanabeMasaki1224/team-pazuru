using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 図形をドラッグ＆ドロップで制御するコンポーネント。
/// 
/// 【概要】
/// - 図形をマウスでドラッグして移動できる。
/// - 他の図形と重なっている場合は配置不可（灰色表示）。
/// - 「Trash」タグを持つオブジェクトに重なってドロップした場合は削除。
/// - 白:置ける 灰色:置けない 赤:削除対象（ゴミ箱上）
/// - ドラッグ中は最前面に表示される。
/// 
/// 【前提条件】
/// - シーン内に EventSystem が存在すること。
/// - カメラに Physics2DRaycaster がアタッチされていること。
/// - 図形オブジェクトに Collider2D と SpriteRenderer があること。
/// </summary>
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class SelectShapes : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    Vector3 _originPosition;       // ドラッグ開始位置
    Collider2D _collider;          // 自身のCollider
    SpriteRenderer _renderer;      // 色制御用
    bool _canPut = true;           // 配置可能かどうかフラグ
    bool _isOverTrash = false;     // ゴミにするかどうかフラグ
    int _defaultOrder;             // 元の描画順序
    [SerializeField] int _dragOrder = 30; // ドラッグ中の最前面順序（他より高く設定）

    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();

        if (FindAnyObjectByType<EventSystem>() == null)
            Debug.LogWarning("EventSystem がシーン内に存在しません。");
    }

    /// <summary>
    /// ドラッグ開始時に元の位置と描画順序を保存し、最前面に持ってくる。
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log($"OnBeginDrag: {name}");

        _originPosition = transform.position;
        _defaultOrder = _renderer.sortingOrder;
        _renderer.sortingOrder = _dragOrder; // 最前面に表示
    }

    /// <summary>
    /// ドラッグ中はマウスに追従し、他のオブジェクトとの重なりをチェック。
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        // マウス位置に追従
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
        mousePos.z = 0;
        transform.position = mousePos;

        // 重なりチェック
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, _collider.bounds.size, 0f);

        _canPut = true;
        _isOverTrash = false;

        foreach (var hit in hits)
        {
            if (hit == _collider) continue; // 自分自身は無視

            // Trashタグの上にあるなら削除対象
            if (hit.CompareTag("Trash"))
            {
                _isOverTrash = true;
                _renderer.color = Color.red;
                return;
            }

            // 他の図形と重なっているなら配置不可
            _canPut = false;
            break;
        }

        _renderer.color = _canPut ? Color.white : Color.grey;
    }

    /// <summary>
    /// ドラッグ終了時に配置状態を確定し、元の描画順序に戻す。
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        _renderer.sortingOrder = _defaultOrder; // 描画順序を元に戻す

        if (_isOverTrash)
        {
            Debug.Log("ゴミ箱に入ったため削除");
            Destroy(gameObject);
            return;
        }

        if (_canPut)
        {
            Debug.Log("配置成功！");
        }
        else
        {
            Debug.Log("配置できません");
            transform.position = _originPosition;
        }

        _renderer.color = Color.white;
    }
}
