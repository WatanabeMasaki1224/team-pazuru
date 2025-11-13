using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 既存のButtonInteractionをラップして、GameStateに応じて有効/無効を制御
/// ButtonInteractionと同じGameObjectにアタッチする
/// </summary>
[RequireComponent(typeof(ButtonInteraction))]
[RequireComponent(typeof(Button))]
public class WrapButton : MonoBehaviour
{
    [Header("有効化条件")]
    [SerializeField] GameState[] _activeStates;

    ButtonInteraction _buttonInteraction;
    Button _button;
    bool _isEnabled;
    GameState _lastState;

    void Start()
    {
        _buttonInteraction = GetComponent<ButtonInteraction>();
        _button = GetComponent<Button>();

        // 初期状態を設定
        if (GameManager.instance != null)
        {
            _lastState = GameManager.instance.CurrentState;
            CheckAndUpdateState(GameManager.instance.CurrentState);
        }
    }

    void Update()
    {
        // GameManagerの状態が変わったかチェック
        if (GameManager.instance != null && GameManager.instance.CurrentState != _lastState)
        {
            _lastState = GameManager.instance.CurrentState;
            CheckAndUpdateState(_lastState);
        }
    }

    void CheckAndUpdateState(GameState newState)
    {
        bool shouldBeEnabled = System.Array.Exists(_activeStates, state => state == newState);

        if (shouldBeEnabled != _isEnabled)
        {
            _isEnabled = shouldBeEnabled;
            UpdateButtonState();
        }
    }

    void UpdateButtonState()
    {
        _button.interactable = _isEnabled;

        if (!_isEnabled)
        {
            // 無効化前にOnPointerExitを強制的に発火してホバー状態を解除
            if (_buttonInteraction.enabled)
            {
                ExecuteEvents.Execute(
                    gameObject,
                    new PointerEventData(EventSystem.current),
                    ExecuteEvents.pointerExitHandler
                );
            }
            _buttonInteraction.enabled = false;
        }
        else
        {
            _buttonInteraction.enabled = true;
        }
    }
}