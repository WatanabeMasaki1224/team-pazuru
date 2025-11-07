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
    [Tooltip("ボタンを押したときに呼ばれるイベント")]
    [SerializeField] UnityEngine.Events.UnityEvent onStageSelected;

    Button _button;

    protected override void Start()
    {
        base.Start();
        _button = GetComponent<Button>();
        UpdateButtonState();
    }

    /// <summary>
    /// ロック状態に応じてボタンの操作可否を切り替える。
    /// </summary>
    protected override void UpdateVisual()
    {
        base.UpdateVisual();
        UpdateButtonState();
    }

    void UpdateButtonState()
    {
        if (_button != null)
        {
            _button.interactable = isUnlocked;
        }
    }

    public override void SetUnlocked(bool unlocked)
    {
        base.SetUnlocked(unlocked);
        UpdateButtonState();
    }

    /// <summary>
    /// クリックされたときの動作（解放済みのみ有効）。
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isUnlocked) return;
        onStageSelected?.Invoke();
    }
}
