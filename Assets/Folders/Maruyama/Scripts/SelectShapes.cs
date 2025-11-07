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
/// - シーン内に EventSystem と ShapeSelectionManager が存在すること。
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

    // 選択/回転/ドラッグの制御用フィールド
    private bool _isSelected = false; // 自身の現在の選択状態 (マネージャーから設定される)
    private float _rotationSpeed = 90f;
    private bool _isDragging = false;

    // マネージャーへの参照
    private ShapeSelectionManager _manager;

    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _visual = GetComponent<ShapeVisualController>();

        // マネージャーの参照を取得
        _manager = FindAnyObjectByType<ShapeSelectionManager>();
        if (_manager == null) Debug.LogError("ShapeSelectionManager がシーン内に存在しません。");
        if (FindAnyObjectByType<EventSystem>() == null)
            Debug.LogWarning("EventSystem がシーン内に存在しません。");

        _manager.SelectShape(this);
    }

    /// <summary>
    /// マネージャーから呼ばれるための公開メソッド。
    /// 自身の選択状態フラグを更新し、ドラッグ中でなければ色を即座に反映します。
    /// </summary>
    public void SetSelectedState(bool isSelected)
    {
        _isSelected = isSelected;

        // ドラッグ中でなければ、マネージャーからの指示を直ちに色に反映
        if (!_isDragging)
        {
            if (isSelected)
            {
                _visual.SetSelectionColor(true);
            }
            else
            {
                _visual.ResetColor();
            }
        }
    }

    void Update()
    {
        bool isPlacing = GameManager.instance.CurrentState == GameState.Placing;

        // 1. Placing状態でなければ全て解除
        if (!isPlacing)
        {
            if (_isSelected)
            {
                // マネージャー経由で解除（_isSelectedもfalseになり、色が白に戻る）
                _manager.DeselectShape(this);
            }
            return;
        }

        // 2. 選択中、かつドラッグ中でない場合のみ回転可能（右クリック長押し）
        if (_isSelected
            && !_isDragging
            && Mouse.current != null
            && Mouse.current.rightButton.isPressed)
        {
            transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// 左クリックで選択/非選択を切り替える。
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        // 【簡略化】排他制御をマネージャーに一任
        _manager.SelectShape(this);
    }

    /// <summary>
    /// ドラッグ開始時に元の位置と描画順序を保存し、最前面に持ってくる。
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 選択中でなければ、ここで処理を終了し、ドラッグを無効化
        if (!_isSelected)
        {
            return;
        }

        _originPosition = transform.position;
        _visual.SetDragOrder();

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
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        _isDragging = false;
        _visual.ResetSortingOrder();

        // 1. ゴミ箱チェック（最優先）
        if (_isOverTrash)
        {
            _manager.DeselectShape(this);
            Destroy(gameObject);
            return;
        }

        // 2. 配置可否チェック
        if (!_canPut)
        {
            transform.position = _originPosition;
        }

        // 3. 選択状態を復元する
        if (_isSelected)
        {
            _manager.SelectShape(this);
        }
        else
        {
            _visual.ResetColor();
        }
    }
}