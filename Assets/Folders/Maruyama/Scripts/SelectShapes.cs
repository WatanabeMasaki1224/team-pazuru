using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

/// <summary>
/// 図形をドラッグ＆ドロップで制御するコンポーネント。
/// 
/// 【概要】
/// - 図形をマウスでドラッグして移動できる。
/// - 他の図形と重なっている場合は配置不可（灰色表示）。
/// - 「Trash」タグを持つオブジェクトに重なってドロップした場合は削除。
/// - 白:置ける 灰色:置けない 赤:削除対象（ゴミ箱上）
/// - 選択中: 黄色で枠 or 明るく表示される
/// - 右クリック長押しでゆっくり回転
/// - ドラッグ中は最前面に表示される。
/// 
/// 【前提条件】
/// - シーン内に EventSystem が存在すること。
/// - カメラに Physics2DRaycaster がアタッチされていること。
/// - 図形オブジェクトに Collider2D と SpriteRenderer があること。
/// </summary>
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(ShapeVisualController))]
public class SelectShapes : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    Vector3 _originPosition;       // ドラッグ開始位置
    Collider2D _collider;          // 自身のCollider
    ShapeVisualController _visual; // 色・描画順管理
    bool _canPut = true;           // 配置可能かどうかフラグ
    bool _isOverTrash = false;     // ゴミにするかどうかフラグ

    bool _isSelected = false;      // 選択状態フラグ
    float _rotationSpeed = 90f;    // 回転速度（度/秒）
    static SelectShapes _currentSelected; // 現在選択中の図形
    // ドラッグ中かどうかを判定するフラグ
    bool _isDragging = false;

    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _visual = GetComponent<ShapeVisualController>();

        if (FindAnyObjectByType<EventSystem>() == null)
            Debug.LogWarning("EventSystem がシーン内に存在しません。");
    }

    void Update()
    {
        bool isPlacing = GameManager.instance.CurrentState == GameState.Placing;

        // 1. Placing状態でなければ全て解除
        if (!isPlacing)
        {
            if (_isSelected)
            {
                _isSelected = false;
                _visual.ResetColor();
                if (_currentSelected == this) _currentSelected = null;
            }
            return;
        }

        // ドラッグ中ではない場合のみ、選択中の色を制御する
        if (!_isDragging)
        {
            // 2. 選択中の視覚フィードバックを設定
            if (_isSelected)
            {
                _visual.SetSelectionColor(true);
            }
            else
            {
                _visual.ResetColor();
            }
        }

        // 3. 選択中、かつドラッグ中でない場合のみ回転可能（右クリック長押し）
        if (_isSelected
            && !_isDragging // ドラッグ中でない
            && Mouse.current != null
            && Mouse.current.rightButton.isPressed)
        {
            transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// 左クリックで選択/非選択を切り替える。（回転・黄色化のトリガー）
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        // 以前の選択を解除
        if (_currentSelected != null && _currentSelected != this)
        {
            _currentSelected._isSelected = false;
            _currentSelected._visual.ResetColor();
        }

        // 自分を選択
        _isSelected = true;
        _currentSelected = this;
        _visual.SetSelectionColor(true); // クリックと同時に色を設定
    }

    /// <summary>
    /// ドラッグ開始時に元の位置と描画順序を保存し、最前面に持ってくる。
    /// この時点で黄色表示を一時的に解除する。
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        _originPosition = transform.position;
        _visual.SetDragOrder();

        // 【追加】ドラッグ開始
        _isDragging = true;

        // 一時的に選択色をリセットして、ドラッグ用フィードバックを優先
        _visual.ResetColor();
    }

    /// <summary>
    /// ドラッグ中はマウスに追従し、他のオブジェクトとの重なりをチェック。
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
        mousePos.z = 0;
        transform.position = mousePos;

        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, _collider.bounds.size, 0f);

        _canPut = true;
        _isOverTrash = false;

        foreach (var hit in hits)
        {
            if (hit == _collider) continue;
            if (hit.CompareTag("Trash"))
            {
                _isOverTrash = true;
                break;
            }
            _canPut = false;
            break;
        }

        // ドラッグ中は常にフィードバック色を適用
        _visual.SetFeedbackColor(_isOverTrash, _canPut);
    }

    /// <summary>
    /// ドラッグ終了時に配置状態を確定し、元の描画順序に戻す。
    /// 最後に、回転・選択可能な状態（黄色）に戻す。
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        // 1.ドラッグ終了フラグをリセット
        _isDragging = false;

        // 2.描画順序を元に戻す
        _visual.ResetSortingOrder();

        // 3.ゴミ箱チェック（最優先）
        if (_isOverTrash)
        {
            // staticから自分を解除
            if (_currentSelected == this)
            {
                _currentSelected = null;
            }
            Destroy(gameObject); // 削除
            return;
        }

        // 4.配置可否チェック
        if (!_canPut)
        {
            // 配置不可なら元の位置に戻す
            transform.position = _originPosition;
        }

        // 5.選択状態と色を復元する
        if (_isSelected)
        {
            // 選択状態を維持し、排他制御を更新して色を復元
            if (_currentSelected != null && _currentSelected != this)
            {
                _currentSelected._isSelected = false;
                _currentSelected._visual.ResetColor();
            }
            _currentSelected = this;
            _visual.SetSelectionColor(true);
        }
        else
        {
            // 非選択状態を維持し、デフォルト色を復元
            _visual.ResetColor();
        }
    }
}
