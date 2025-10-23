using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 図形をドラッグ＆ドロップで制御（簡易版）
/// シーン内にEventSystemが必要
/// カメラに Physics2DRaycaster をアタッチする必要がある
/// オブジェクトには Collider2D が必要
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class SelectShapes : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector3 _originPosition; // 生成されるポジション
    bool _canPut = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // ドラッグ開始時に元の位置を保存
        _originPosition = transform.position;
        Debug.Log("ドラッグ開始");
    }

    public void OnDrag(PointerEventData eventData)
    {
        // マウスのスクリーン座標をワールド座標に変換して追従
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
        mousePos.z = 0; // 2DなのでZ固定
        transform.position = mousePos;

        Debug.Log("ドラッグ中");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("ドラッグ終了");
        // 今は終了時の位置固定。必要なら元に戻すことも可能
        // transform.position = _originPosition;
        // ここで_canPut = true なら位置更新
    }

    void Start()
    {
        // EventSystemチェック
        if (FindAnyObjectByType<EventSystem>() == null)
            Debug.LogWarning("シーン内に EventSystem が存在しません。");

        // Physics2DRaycasterチェック
        Camera cam = Camera.main;
        if (cam != null && cam.GetComponent<Physics2DRaycaster>() == null)
            Debug.LogWarning("カメラに Physics2DRaycaster がアタッチされていません。");
    }
}
