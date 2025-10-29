using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

/// <summary>
/// �}�`���h���b�O���h���b�v�Ő��䂷��R���|�[�l���g�B
/// 
/// �y�T�v�z
/// - �}�`���}�E�X�Ńh���b�O���Ĉړ��ł���B
/// - ���̐}�`�Əd�Ȃ��Ă���ꍇ�͔z�u�s�i�D�F�\���j�B
/// - �uTrash�v�^�O�����I�u�W�F�N�g�ɏd�Ȃ��ăh���b�v�����ꍇ�͍폜�B
/// - ��:�u���� �D�F:�u���Ȃ� ��:�폜�Ώہi�S�~����j
/// - �I��: ���F�Řg or ���邭�\�������
/// - �E�N���b�N�������ł�������]
/// - �h���b�O���͍őO�ʂɕ\�������B
/// 
/// �y�O������z
/// - �V�[������ EventSystem �����݂��邱�ƁB
/// - �J������ Physics2DRaycaster ���A�^�b�`����Ă��邱�ƁB
/// - �}�`�I�u�W�F�N�g�� Collider2D �� SpriteRenderer �����邱�ƁB
/// </summary>
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(ShapeVisualController))]
public class SelectShapes : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    Vector3 _originPosition;       // �h���b�O�J�n�ʒu
    Collider2D _collider;          // ���g��Collider
    ShapeVisualController _visual; // �F�E�`�揇�Ǘ�
    bool _canPut = true;           // �z�u�\���ǂ����t���O
    bool _isOverTrash = false;     // �S�~�ɂ��邩�ǂ����t���O

    bool _isSelected = false;      // �I����ԃt���O
    float _rotationSpeed = 90f;    // ��]���x�i�x/�b�j
    static SelectShapes _currentSelected; // ���ݑI�𒆂̐}�`
    // �h���b�O�����ǂ����𔻒肷��t���O
    bool _isDragging = false;

    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _visual = GetComponent<ShapeVisualController>();

        if (FindAnyObjectByType<EventSystem>() == null)
            Debug.LogWarning("EventSystem ���V�[�����ɑ��݂��܂���B");
    }

    void Update()
    {
        bool isPlacing = GameManager.instance.CurrentState == GameState.Placing;

        // 1. Placing��ԂłȂ���ΑS�ĉ���
        if (!isPlacing)
        {
            if (_isSelected)
            {
                _isSelected = false;
                _visual.ResetColor();
                if (_currentSelected == this) _currentSelected = null;
            }
            return;
        }

        // �h���b�O���ł͂Ȃ��ꍇ�̂݁A�I�𒆂̐F�𐧌䂷��
        if (!_isDragging)
        {
            // 2. �I�𒆂̎��o�t�B�[�h�o�b�N��ݒ�
            if (_isSelected)
            {
                _visual.SetSelectionColor(true);
            }
            else
            {
                _visual.ResetColor();
            }
        }

        // 3. �I�𒆁A���h���b�O���łȂ��ꍇ�̂݉�]�\�i�E�N���b�N�������j
        if (_isSelected
            && !_isDragging // �h���b�O���łȂ�
            && Mouse.current != null
            && Mouse.current.rightButton.isPressed)
        {
            transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// ���N���b�N�őI��/��I����؂�ւ���B�i��]�E���F���̃g���K�[�j
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        // �ȑO�̑I��������
        if (_currentSelected != null && _currentSelected != this)
        {
            _currentSelected._isSelected = false;
            _currentSelected._visual.ResetColor();
        }

        // ������I��
        _isSelected = true;
        _currentSelected = this;
        _visual.SetSelectionColor(true); // �N���b�N�Ɠ����ɐF��ݒ�
    }

    /// <summary>
    /// �h���b�O�J�n���Ɍ��̈ʒu�ƕ`�揇����ۑ����A�őO�ʂɎ����Ă���B
    /// ���̎��_�ŉ��F�\�����ꎞ�I�ɉ�������B
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        _originPosition = transform.position;
        _visual.SetDragOrder();

        // �y�ǉ��z�h���b�O�J�n
        _isDragging = true;

        // �ꎞ�I�ɑI��F�����Z�b�g���āA�h���b�O�p�t�B�[�h�o�b�N��D��
        _visual.ResetColor();
    }

    /// <summary>
    /// �h���b�O���̓}�E�X�ɒǏ]���A���̃I�u�W�F�N�g�Ƃ̏d�Ȃ���`�F�b�N�B
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
        mousePos.z = 0;
        transform.position = mousePos;

        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, _collider.bounds.size, 0f);

        _canPut = true;
        _isOverTrash = false;

        foreach (var hit in hits)
        {
            if (hit == _collider) continue;
            if (hit.CompareTag("Trash"))
            {
                _isOverTrash = true;
                break;
            }
            _canPut = false;
            break;
        }

        // �h���b�O���͏�Ƀt�B�[�h�o�b�N�F��K�p
        _visual.SetFeedbackColor(_isOverTrash, _canPut);
    }

    /// <summary>
    /// �h���b�O�I�����ɔz�u��Ԃ��m�肵�A���̕`�揇���ɖ߂��B
    /// �Ō�ɁA��]�E�I���\�ȏ�ԁi���F�j�ɖ߂��B
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        // 1.�h���b�O�I���t���O�����Z�b�g
        _isDragging = false;

        // 2.�`�揇�������ɖ߂�
        _visual.ResetSortingOrder();

        // 3.�S�~���`�F�b�N�i�ŗD��j
        if (_isOverTrash)
        {
            // static���玩��������
            if (_currentSelected == this)
            {
                _currentSelected = null;
            }
            Destroy(gameObject); // �폜
            return;
        }

        // 4.�z�u�ۃ`�F�b�N
        if (!_canPut)
        {
            // �z�u�s�Ȃ猳�̈ʒu�ɖ߂�
            transform.position = _originPosition;
        }

        // 5.�I����ԂƐF�𕜌�����
        if (_isSelected)
        {
            // �I����Ԃ��ێ����A�r��������X�V���ĐF�𕜌�
            if (_currentSelected != null && _currentSelected != this)
            {
                _currentSelected._isSelected = false;
                _currentSelected._visual.ResetColor();
            }
            _currentSelected = this;
            _visual.SetSelectionColor(true);
        }
        else
        {
            // ��I����Ԃ��ێ����A�f�t�H���g�F�𕜌�
            _visual.ResetColor();
        }
    }
}
