using UnityEngine;
using UnityEngine.EventSystems;

public class GimmikDragHandler : MonoBehaviour, IDragHandler
{
	/// <summary>
	/// �h���b�O���i�}�E�X�{�^�����������܂܈ړ��j�Ƀt���[�����ƂɌĂ΂��
	/// </summary>
	public void OnDrag(PointerEventData eventData)
	{
		Vector3 mouseWorldPosition = GetMouseWorldPosition(eventData.position);

		transform.position = mouseWorldPosition;
	}

	/// <summary>
	/// �X�N���[�����W�i�}�E�X�ʒu�j�����[���h���W�ɕϊ�����w���p�[�֐�
	/// </summary>
	private Vector3 GetMouseWorldPosition(Vector2 screenPosition)
	{
		Vector3 viewPortPoint = new Vector3(
			screenPosition.x,
			screenPosition.y,
			Camera.main.WorldToScreenPoint(transform.position).z // �I�u�W�F�N�g��Z�[�x���ێ�
		);
		return Camera.main.ScreenToWorldPoint(viewPortPoint);
	}
}