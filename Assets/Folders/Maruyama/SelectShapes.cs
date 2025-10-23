using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// �}�`���h���b�O���h���b�v�Ő���i�ȈՔŁj
/// �V�[������EventSystem���K�v
/// �J������ Physics2DRaycaster ���A�^�b�`����K�v������
/// �I�u�W�F�N�g�ɂ� Collider2D ���K�v
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class SelectShapes : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector3 _originPosition; // ���������|�W�V����
    bool _canPut = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // �h���b�O�J�n���Ɍ��̈ʒu��ۑ�
        _originPosition = transform.position;
        Debug.Log("�h���b�O�J�n");
    }

    public void OnDrag(PointerEventData eventData)
    {
        // �}�E�X�̃X�N���[�����W�����[���h���W�ɕϊ����ĒǏ]
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
        mousePos.z = 0; // 2D�Ȃ̂�Z�Œ�
        transform.position = mousePos;

        Debug.Log("�h���b�O��");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("�h���b�O�I��");
        // ���͏I�����̈ʒu�Œ�B�K�v�Ȃ猳�ɖ߂����Ƃ��\
        // transform.position = _originPosition;
        // ������_canPut = true �Ȃ�ʒu�X�V
    }

    void Start()
    {
        // EventSystem�`�F�b�N
        if (FindAnyObjectByType<EventSystem>() == null)
            Debug.LogWarning("�V�[������ EventSystem �����݂��܂���B");

        // Physics2DRaycaster�`�F�b�N
        Camera cam = Camera.main;
        if (cam != null && cam.GetComponent<Physics2DRaycaster>() == null)
            Debug.LogWarning("�J������ Physics2DRaycaster ���A�^�b�`����Ă��܂���B");
    }
}
