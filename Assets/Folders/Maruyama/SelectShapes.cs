using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 図形をドラッグ＆ドロップで制御するコンポーネント。
/// 
/// 【概要】
/// - 図形をマウスでドラッグして移動できる。
/// - 他の図形と重なっている場合は配置不可。
/// - 配置できない場合は元の位置に戻る。
/// - ドラッグ中は置けるときだけ赤く表示。
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
    Vector3 _originPosition;
    Collider2D _collider;
    SpriteRenderer _renderer;
    bool _canPut = true;

    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();

        if (FindAnyObjectByType<EventSystem>() == null)
            Debug.LogWarning("EventSystem がシーン内に存在しません。");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _originPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
        mousePos.z = 0;
        transform.position = mousePos;

        // 自分以外の重なりをチェック
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, _collider.bounds.size, 0f);
        _canPut = true;
        foreach (var hit in hits)
        {
            if (hit != _collider)
            {
                _canPut = false;
                break;
            }
        }

        // 置けないとき赤くする
        _renderer.color = _canPut ? Color.white : Color.red;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_canPut)
        {
            Debug.Log("配置成功！");
        }
        else
        {
            Debug.Log("配置できません（重なり）");
            transform.position = _originPosition;
        }

        // ドラッグ終了時は色を元に戻す
        _renderer.color = Color.white;
    }
}
