using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// �}�`���h���b�O���h���b�v�Ő��䂷��R���|�[�l���g�B
/// 
/// �y�T�v�z
/// - �}�`���}�E�X�Ńh���b�O���Ĉړ��ł���B
/// - ���̐}�`�Əd�Ȃ��Ă���ꍇ�͔z�u�s�B
/// - �z�u�ł��Ȃ��ꍇ�͌��̈ʒu�ɖ߂�B
/// - �h���b�O���͒u����Ƃ������Ԃ��\���B
/// 
/// �y�O������z
/// - �V�[������ EventSystem �����݂��邱�ƁB
/// - �J������ Physics2DRaycaster ���A�^�b�`����Ă��邱�ƁB
/// - �}�`�I�u�W�F�N�g�� Collider2D �� SpriteRenderer �����邱�ƁB
/// </summary>
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class SelectShapes : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    Vector3 _originPosition;
    Collider2D _collider;
    SpriteRenderer _renderer;
    bool _canPut = true;

    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();

        if (FindAnyObjectByType<EventSystem>() == null)
            Debug.LogWarning("EventSystem ���V�[�����ɑ��݂��܂���B");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _originPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
        mousePos.z = 0;
        transform.position = mousePos;

        // �����ȊO�̏d�Ȃ���`�F�b�N
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, _collider.bounds.size, 0f);
        _canPut = true;
        foreach (var hit in hits)
        {
            if (hit != _collider)
            {
                _canPut = false;
                break;
            }
        }

        // �u���Ȃ��Ƃ��Ԃ�����
        _renderer.color = _canPut ? Color.white : Color.red;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_canPut)
        {
            Debug.Log("�z�u�����I");
        }
        else
        {
            Debug.Log("�z�u�ł��܂���i�d�Ȃ�j");
            transform.position = _originPosition;
        }

        // �h���b�O�I�����͐F�����ɖ߂�
        _renderer.color = Color.white;
    }
}
