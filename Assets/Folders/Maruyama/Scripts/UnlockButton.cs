using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// ステージのアンロック状態を管理し、
/// ボタンの有効/無効と見た目を制御する。
/// </summary>
[RequireComponent(typeof(Button))]
public class UnlockButton : UnlockableUI, IPointerClickHandler
{
    [Tooltip("このボタンが対応するステージ番号")]
    [SerializeField] int stageNumber = 1;

    [Tooltip("ボタンを押したときに呼ばれるイベント")]
    [SerializeField] UnityEngine.Events.UnityEvent onStageSelected;

    Button _button;
    ButtonInteraction _interaction;
    Color _defaultColor; // ← 元の色を保持しておく
    Color _defaultImageColor;
    ColorBlock _defaultColorBlock;

    void Awake()
    {
        // 早めに取得しておく（Start より前）
        _button = GetComponent<Button>();
        _interaction = GetComponent<ButtonInteraction>();

        if (targetImage != null)
        {
            _defaultImageColor = targetImage.color;
        }

        if (_button != null)
        {
            _defaultColorBlock = _button.colors; // Button の ColorBlock（normal/highlight 等）
        }
    }

    protected override void Start()
    {
        base.Start();

        // 現在のアンロック状況を確認（Startでやる）
        int unlockedStage = PlayerPrefs.GetInt("UnlockedStage", 1);
        bool unlocked = stageNumber <= unlockedStage;

        SetUnlocked(unlocked);
    }

    protected override void UpdateVisual()
    {
        if (targetImage == null) return;

        if (isUnlocked)
        {
            // Image の色を戻す
            targetImage.color = _defaultImageColor;

            // Button の ColorBlock も元に戻す（これが緑の原因ならここで復元）
            if (_button != null)
            {
                _button.colors = _defaultColorBlock;
            }
        }
        else
        {
            var color = lockedColor;
            color.a = lockedAlpha;
            targetImage.color = color;

            // ロック時にボタンの標準色を薄くする (任意)
            if (_button != null)
            {
                var cb = _button.colors;
                cb.normalColor = color;
                cb.highlightedColor = color;
                cb.pressedColor = color;
                _button.colors = cb;
            }
        }
    }

    void UpdateButtonState()
    {
        if (_button != null)
            _button.interactable = isUnlocked;

        if (_interaction != null)
            _interaction.enabled = isUnlocked;
    }

    public override void SetUnlocked(bool unlocked)
    {
        isUnlocked = unlocked;
        UpdateVisual();
        UpdateButtonState();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isUnlocked) return;
        onStageSelected?.Invoke();
    }
}
