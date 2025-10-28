using UnityEngine;

/// <summary>
/// �}�`�̎��o�I�\���i�F�A�`�揇���j�𐧌䂷��R���|�[�l���g�B
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class ShapeVisualController : MonoBehaviour
{
    SpriteRenderer _renderer;
    int _defaultOrder;

    // �J�v�Z����
    [SerializeField] int _dragOrder = 30;

    [Header("Color Settings")]
    [SerializeField] Color _defaultColor = Color.white;
    [SerializeField] Color _canPutColor = Color.white;
    [SerializeField] Color _cannotPutColor = Color.grey;
    [SerializeField] Color _trashColor = Color.red;
    [SerializeField] Color _selectedColor = new Color(1f, 1f, 0.5f, 1f);

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _defaultOrder = _renderer.sortingOrder;
    }

    /// <summary>
    /// �h���b�O���̔z�u�\��Ԃɉ������F��ݒ肵�܂��B
    /// �D�揇��: �S�~�� (��) > �z�u�s�� (�D) > �z�u�� (��)
    /// </summary>
    public void SetFeedbackColor(bool isOverTrash, bool canPut)
    {
        if (isOverTrash)
        {
            _renderer.color = _trashColor; // ��
        }
        else
        {
            _renderer.color = canPut ? _canPutColor : _cannotPutColor; // �� �܂��� �D�F
        }
    }

    /// <summary>
    /// �I����Ԃɉ����ĐF��ݒ肵�܂��B�i�h���b�O�O�̃N���b�N���p�j
    /// </summary>
    public void SetSelectionColor(bool isSelected)
    {
        _renderer.color = isSelected ? _selectedColor : _defaultColor;
    }

    /// <summary>
    /// �h���b�O�J�n���̕`�揇����ݒ肵�܂��B
    /// </summary>
    public void SetDragOrder()
    {
        _renderer.sortingOrder = _dragOrder;
    }

    /// <summary>
    /// �h���b�O�I�����̌��̕`�揇���ɖ߂��܂��B
    /// </summary>
    public void ResetSortingOrder()
    {
        _renderer.sortingOrder = _defaultOrder;
    }

    /// <summary>
    /// �����F�i���j��ݒ肵�܂��B
    /// </summary>
    public void ResetColor()
    {
        _renderer.color = _defaultColor;
    }
}