using UnityEngine;

/// <summary>
/// ���̃y�A���Ƃɋ�̐e�I�u�W�F�N�g�ɃZ�b�g�œ����
/// ���̃��[�v�����ƃy�A�̈ʒu�𑨂��鏈�����s��
/// </summary>
public class GimmikStarManager : MonoBehaviour
{
    [SerializeField] string _pairStarName = default;
	Vector2 _pairStarPos;

    void Start()
    {
		Transform parentTransform = transform.parent; // ���g�̐e�I�u�W�F�N�g���擾

		if (parentTransform)
		{
			_pairStarPos = parentTransform.Find(_pairStarName).position;	// �y�A�ƂȂ鐯�I�u�W�F�N�g�̈ʒu�����

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
		if(collision.tag == "Ball")
		{
			collision.transform.position = _pairStarPos;
		}
	}
}
