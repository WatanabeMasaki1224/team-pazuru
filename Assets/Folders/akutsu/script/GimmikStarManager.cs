using UnityEngine;

/// <summary>
/// ���̃y�A���Ƃɋ�̐e�I�u�W�F�N�g�ɃZ�b�g�œ����
/// ���̃��[�v�����ƃy�A�̈ʒu�𑨂��鏈�����s��
/// </summary>
public class GimmikStarManager : MonoBehaviour
{
	[SerializeField] GameObject _partner;
	Vector2 _pairStarPos;
	StarParentManager _parentManager;

    void Start()
    {
		_parentManager = GetComponentInParent<StarParentManager>();
		if (_parentManager == null)
		{
			Debug.LogError("�e�I�u�W�F�N�g�̃X�N���v�g��������܂���B");
			return;
		}

		Transform parentTransform = transform.parent; // ���g�̐e�I�u�W�F�N�g���擾

		if (parentTransform)
		{
			_pairStarPos = parentTransform.Find(_partner.name).position;	// �y�A�ƂȂ鐯�I�u�W�F�N�g�̈ʒu�����

			if (_pairStarPos != null)
			{
				Debug.Log($"{transform.name}�̃y�A�i{_pairStarPos}�j�̈ʒu�������܂���: {_pairStarPos}");
			}
			else
			{
				Debug.LogError($"�y�A�ƂȂ�I�u�W�F�N�g��������܂���: {_pairStarPos}");
			}
		}
		else
		{
			Debug.LogError("���̃��[�v�|�C���g�ɂ͐e�I�u�W�F�N�g������܂���B");
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Ball"�@&& _parentManager.IsWarp) // �e�I�u�W�F�N�g�̃X�N���v�g���烏�[�v�ł��邩�m�F
		{
			collision.transform.position = _pairStarPos;

			_parentManager.WarpRestriction(); // �A�����[�v�ɂȂ�Ȃ��悤�ɐ�����������
		}
	}
}
