using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI要素に「アンロック状態」を付与するための基底クラス。
/// 継承先でフラグの条件や追加演出を実装する。
/// </summary>
public abstract class UnlockableUI : MonoBehaviour
{
    [SerializeField] protected bool isUnlocked;
    [SerializeField] protected Image targetImage;
    [SerializeField] protected Color lockedColor = Color.black;
    [SerializeField, Range(0f, 1f)] protected float lockedAlpha = 0.1f;

    /// <summary>
    /// 開始時に見た目を更新する。
    /// </summary>
    protected virtual void Start()
    {
        UpdateVisual();
    }

    /// <summary>
    /// 現在のロック状態に応じて見た目を更新する。
    /// </summary>
    protected virtual void UpdateVisual()
    {
        if (targetImage == null) return;

        var color = targetImage.color;
        if (isUnlocked)
        {
            // アンロック時は元の色のまま
            color.a = 1f;
        }
        else
        {
            // ロック時は暗く・透明に
            color = lockedColor;
            color.a = lockedAlpha;
        }

        targetImage.color = color;
    }

    /// <summary>
    /// ロック状態を設定し、見た目を更新する。
    /// </summary>
    public virtual void SetUnlocked(bool unlocked)
    {
        isUnlocked = unlocked;
        UpdateVisual();
    }
}
