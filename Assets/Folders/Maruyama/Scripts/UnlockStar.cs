using UnityEngine;

/// <summary>
/// ステージのクリア状況に応じて星の見た目を切り替える。
/// </summary>
public class UnlockStar : UnlockableUI
{
    protected override void Start()
    {
        base.Start();
        UpdateVisual();
    }

    protected override void UpdateVisual()
    {
        base.UpdateVisual();

        if (targetImage == null) return;

        var color = targetImage.color;
        color.a = isUnlocked ? 1f : lockedAlpha;
        targetImage.color = color;
    }

    public override void SetUnlocked(bool unlocked)
    {
        base.SetUnlocked(unlocked);
        UpdateVisual();
    }
}
