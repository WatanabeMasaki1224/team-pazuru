using UnityEngine;
using UnityEngine.Events;

public class MenuButtonManager : MonoBehaviour
{
    [SerializeField] UnityEvent _onClick = default;

	public void ExecuteClick()
	{
		_onClick.Invoke();
	}
}
