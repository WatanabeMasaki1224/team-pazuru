using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// UI Button �ȊO��OnClick()�C�x���g���g�����߂�cs
/// �C�x���g���Ăт����I�u�W�F�N�g�ɃA�^�b�`���Ďg��
/// EventTrigger�R���|�[�l���g�� PointerClick �ƑS���ꏏ������
/// UI �Ȃ�Canvas�z���ŋ@�\
/// �I�u�W�F�N�g�Ȃ� Collider ��t����K�v����
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
