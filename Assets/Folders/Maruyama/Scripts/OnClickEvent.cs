using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// UI Button 以外でOnClick()イベントを使うためのcs
/// イベントを呼びたいオブジェクトにアタッチして使う
/// EventTriggerコンポーネントの PointerClick と全く一緒だった
/// UI ならCanvas配下で機能
/// オブジェクトなら Collider を付ける必要あり
/// </summary>

public class OnClickEvent : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] UnityEvent _onClick = default;

    public void OnPointerClick(PointerEventData eventData)
    {
        _onClick?.Invoke();
    }
    public void SetEvent(UnityEvent e)
    {
        _onClick = e;
    }
}
