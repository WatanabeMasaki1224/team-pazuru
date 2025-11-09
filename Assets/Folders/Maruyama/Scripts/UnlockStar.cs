using UnityEngine;

/// <summary>
/// ステージのクリア状況に応じて星の見た目を切り替える。
/// </summary>
public class UnlockStar : UnlockableUI
{
    [Header("メダル設定")]
    [SerializeField] int stageNumber; // このメダルが対応するステージ番号

    protected override void Start()
    {
        if (targetImage == null)
        {
            Debug.LogError($"{nameof(UnlockStar)}: targetImageが未設定。");
        }

        // PlayerPrefsからメダル獲得状態を読み込み
        LoadMedalStatus();

        // base.Start()は呼ばない（二重更新を防ぐ）
        UpdateVisual();
    }

    /// <summary>
    /// PlayerPrefsからメダル獲得状態を読み込む
    /// </summary>
    private void LoadMedalStatus()
    {
        string stageMedalKey = $"Medal_Stage_{stageNumber}";
        isUnlocked = PlayerPrefs.GetInt(stageMedalKey, 0) == 1;
        Debug.Log($"ステージ{stageNumber}メダル: {(isUnlocked ? "獲得済み" : "未獲得")}");
    }

    protected override void UpdateVisual()
    {
        if (targetImage == null) return;

        if (isUnlocked)
        {
            var color = targetImage.color;
            color.a = 1f;
            targetImage.color = color;
        }
        else
        {
            targetImage.color = new Color(lockedColor.r, lockedColor.g, lockedColor.b, lockedAlpha);
        }
    }

    public override void SetUnlocked(bool unlocked)
    {
        isUnlocked = unlocked;
        UpdateVisual();
    }
}