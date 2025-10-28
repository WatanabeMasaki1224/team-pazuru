using UnityEngine;

/// <summary>
/// 図形の視覚的表現（色、描画順序）を制御するコンポーネント。
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class ShapeVisualController : MonoBehaviour
{
    SpriteRenderer _renderer;
    int _defaultOrder;

    // カプセル化
    [SerializeField] int _dragOrder = 30;

    [Header("Color Settings")]
    [SerializeField] Color _defaultColor = Color.white;
    [SerializeField] Color _canPutColor = Color.white;
    [SerializeField] Color _cannotPutColor = Color.grey;
    [SerializeField] Color _trashColor = Color.red;
    [SerializeField] Color _selectedColor = new Color(1f, 1f, 0.5f, 1f);

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _defaultOrder = _renderer.sortingOrder;
    }

    /// <summary>
    /// ドラッグ中の配置可能状態に応じた色を設定します。
    /// 優先順位: ゴミ箱 (赤) > 配置不可 (灰) > 配置可 (白)
    /// </summary>
    public void SetFeedbackColor(bool isOverTrash, bool canPut)
    {
        if (isOverTrash)
        {
            _renderer.color = _trashColor; // 赤
        }
        else
        {
            _renderer.color = canPut ? _canPutColor : _cannotPutColor; // 白 または 灰色
        }
    }

    /// <summary>
    /// 選択状態に応じて色を設定します。（ドラッグ外のクリック時用）
    /// </summary>
    public void SetSelectionColor(bool isSelected)
    {
        _renderer.color = isSelected ? _selectedColor : _defaultColor;
    }

    /// <summary>
    /// ドラッグ開始時の描画順序を設定します。
    /// </summary>
    public void SetDragOrder()
    {
        _renderer.sortingOrder = _dragOrder;
    }

    /// <summary>
    /// ドラッグ終了時の元の描画順序に戻します。
    /// </summary>
    public void ResetSortingOrder()
    {
        _renderer.sortingOrder = _defaultOrder;
    }

    /// <summary>
    /// 初期色（白）を設定します。
    /// </summary>
    public void ResetColor()
    {
        _renderer.color = _defaultColor;
    }
}