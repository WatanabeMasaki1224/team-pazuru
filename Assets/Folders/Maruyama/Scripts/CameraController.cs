using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ゲームの状態に応じて Physics2DRaycaster を自動で有効・無効にする。
/// カメラにアタッチして使う。
/// </summary>
[RequireComponent(typeof(Physics2DRaycaster))]
public class CameraController : MonoBehaviour
{
    Physics2DRaycaster _raycaster;

    void Awake()
    {
        _raycaster = GetComponent<Physics2DRaycaster>();
    }

    void Update()
    {
        if (GameManager.instance == null) return;

        // Placing フェイズのみ Raycaster 有効
        _raycaster.enabled = (GameManager.instance.CurrentState == GameState.Placing);
    }
}
