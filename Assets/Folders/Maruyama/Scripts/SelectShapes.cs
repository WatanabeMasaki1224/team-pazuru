using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// �}�`���h���b�O���h���b�v�Ő��䂷��R���|�[�l���g�B
/// 
/// �y�T�v�z
/// - �}�`���}�E�X�Ńh���b�O���Ĉړ��ł���B
/// - ���̐}�`�Əd�Ȃ��Ă���ꍇ�͔z�u�s�i�D�F�\���j�B
/// - �uTrash�v�^�O�����I�u�W�F�N�g�ɏd�Ȃ��ăh���b�v�����ꍇ�͍폜�B
/// - ��:�u���� �D�F:�u���Ȃ� ��:�폜�Ώہi�S�~����j
/// - �h���b�O���͍őO�ʂɕ\�������B
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
    Vector3 _originPosition;       // �h���b�O�J�n�ʒu
    Collider2D _collider;          // ���g��Collider
    SpriteRenderer _renderer;      // �F����p
    bool _canPut = true;           // �z�u�\���ǂ����t���O
    bool _isOverTrash = false;     // �S�~�ɂ��邩�ǂ����t���O
    int _defaultOrder;             // ���̕`�揇��
    [SerializeField] int _dragOrder = 30; // �h���b�O���̍őO�ʏ����i����荂���ݒ�j

    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();

        if (FindAnyObjectByType<EventSystem>() == null)
            Debug.LogWarning("EventSystem ���V�[�����ɑ��݂��܂���B");
    }

    /// <summary>
    /// �h���b�O�J�n���Ɍ��̈ʒu�ƕ`�揇����ۑ����A�őO�ʂɎ����Ă���B
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log($"OnBeginDrag: {name}");

        _originPosition = transform.position;
        _defaultOrder = _renderer.sortingOrder;
        _renderer.sortingOrder = _dragOrder; // �őO�ʂɕ\��
    }

    /// <summary>
    /// �h���b�O���̓}�E�X�ɒǏ]���A���̃I�u�W�F�N�g�Ƃ̏d�Ȃ���`�F�b�N�B
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        // �}�E�X�ʒu�ɒǏ]
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
        mousePos.z = 0;
        transform.position = mousePos;

        // �d�Ȃ�`�F�b�N
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, _collider.bounds.size, 0f);

        _canPut = true;
        _isOverTrash = false;

        foreach (var hit in hits)
        {
            if (hit == _collider) continue; // �������g�͖���

            // Trash�^�O�̏�ɂ���Ȃ�폜�Ώ�
            if (hit.CompareTag("Trash"))
            {
                _isOverTrash = true;
                _renderer.color = Color.red;
                return;
            }

            // ���̐}�`�Əd�Ȃ��Ă���Ȃ�z�u�s��
            _canPut = false;
            break;
        }

        _renderer.color = _canPut ? Color.white : Color.grey;
    }

    /// <summary>
    /// �h���b�O�I�����ɔz�u��Ԃ��m�肵�A���̕`�揇���ɖ߂��B
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        _renderer.sortingOrder = _defaultOrder; // �`�揇�������ɖ߂�

        if (_isOverTrash)
        {
            Debug.Log("�S�~���ɓ��������ߍ폜");
            Destroy(gameObject);
            return;
        }

        if (_canPut)
        {
            Debug.Log("�z�u�����I");
        }
        else
        {
            Debug.Log("�z�u�ł��܂���");
            transform.position = _originPosition;
        }

        _renderer.color = Color.white;
    }
}
