// ShapeSelectionManager.cs
using UnityEngine;

/// <summary>
/// 図形の単一選択(排他制御)ロジック、および選択状態の維持を担当するマネージャー。
/// シーン内に1つ存在し、SelectShapesから参照されます。
/// </summary>
public class ShapeSelectionManager : MonoBehaviour
{
    // 現在選択中の図形の参照
    private SelectShapes _currentSelected = null;

    /// <summary>
    /// 指定された図形を選択状態にする(排他制御込み)。
    /// Start()やOnPointerClick、OnEndDragから呼ばれます。
    /// </summary>
    public void SelectShape(SelectShapes newSelection)
    {
        // 以前の選択を解除
        if (_currentSelected != null && _currentSelected != newSelection)
        {
            // 以前の図形に「非選択状態になるように」指示する
            _currentSelected.SetSelectedState(false);
        }

        // 新しい図形を選択状態にする
        _currentSelected = newSelection;
        _currentSelected.SetSelectedState(true);
    }

    /// <summary>
    /// 指定された図形の選択状態を解除する。
    /// ゴミ箱へのドロップ時や、GameManagerの状態がPlacingでなくなったときに呼ばれます。
    /// </summary>
    public void DeselectShape(SelectShapes shape)
    {
        if (_currentSelected == shape)
        {
            shape.SetSelectedState(false);
            _currentSelected = null;
        }
    }
}