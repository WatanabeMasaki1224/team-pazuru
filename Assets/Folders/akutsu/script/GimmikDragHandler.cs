using UnityEngine;
using UnityEngine.EventSystems;

public class GimmikDragHandler : MonoBehaviour, IDragHandler
{
	/// <summary>
	/// ドラッグ中（マウスボタンを押したまま移動）にフレームごとに呼ばれる
	/// </summary>
	public void OnDrag(PointerEventData eventData)
	{
		Vector3 mouseWorldPosition = GetMouseWorldPosition(eventData.position);

		transform.position = mouseWorldPosition;
	}

	/// <summary>
	/// スクリーン座標（マウス位置）をワールド座標に変換するヘルパー関数
	/// </summary>
	private Vector3 GetMouseWorldPosition(Vector2 screenPosition)
	{
		Vector3 viewPortPoint = new Vector3(
			screenPosition.x,
			screenPosition.y,
			Camera.main.WorldToScreenPoint(transform.position).z // オブジェクトのZ深度を維持
		);
		return Camera.main.ScreenToWorldPoint(viewPortPoint);
	}
}