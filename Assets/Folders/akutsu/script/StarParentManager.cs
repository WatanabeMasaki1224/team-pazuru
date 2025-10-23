using System.Collections;
using UnityEngine;

public class StarParentManager : MonoBehaviour
{
    bool _isWarp = true;
    bool _isCoolDown = false;
    float _timer;
    [SerializeField] float _restrictionTime = 2f;

    public bool IsWarp => _isWarp;

    /// <summary>
    /// ���̃��[�v���@�\�����Ƃ��ɌĂяo���֐�
    /// ���[�v�̏����𐧌�����
    /// </summary>
    public void WarpRestriction()
    {
        if (!_isCoolDown)
        {
            _isWarp = false;
            StartCoroutine(CooldownCoroutine());
        }
    }

	IEnumerator CooldownCoroutine()
	{
        _isCoolDown = true;

		yield return new WaitForSeconds(_restrictionTime);

        _isCoolDown = false;

		Debug.Log("���[�v�V�X�e���F�N�[���_�E���I��");

		_isWarp = true;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Ball")
        {
            // �����@�\���Ȃ���ˁH
        }
	}
}
