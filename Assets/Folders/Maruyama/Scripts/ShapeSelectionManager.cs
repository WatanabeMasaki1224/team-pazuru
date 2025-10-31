// ShapeSelectionManager.cs
using UnityEngine;

/// <summary>
/// �}�`�̒P��I��(�r������)���W�b�N�A����ёI����Ԃ̈ێ���S������}�l�[�W���[�B
/// �V�[������1���݂��ASelectShapes����Q�Ƃ���܂��B
/// </summary>
public class ShapeSelectionManager : MonoBehaviour
{
    // ���ݑI�𒆂̐}�`�̎Q��
    private SelectShapes _currentSelected = null;

    /// <summary>
    /// �w�肳�ꂽ�}�`��I����Ԃɂ���(�r�����䍞��)�B
    /// Start()��OnPointerClick�AOnEndDrag����Ă΂�܂��B
    /// </summary>
    public void SelectShape(SelectShapes newSelection)
    {
        // �ȑO�̑I��������
        if (_currentSelected != null && _currentSelected != newSelection)
        {
            // �ȑO�̐}�`�Ɂu��I����ԂɂȂ�悤�Ɂv�w������
            _currentSelected.SetSelectedState(false);
        }

        // �V�����}�`��I����Ԃɂ���
        _currentSelected = newSelection;
        _currentSelected.SetSelectedState(true);
    }

    /// <summary>
    /// �w�肳�ꂽ�}�`�̑I����Ԃ���������B
    /// �S�~���ւ̃h���b�v����AGameManager�̏�Ԃ�Placing�łȂ��Ȃ����Ƃ��ɌĂ΂�܂��B
    /// </summary>
    public void DeselectShape(SelectShapes shape)
    {
        if (_currentSelected == shape)
        {
            shape.SetSelectedState(false);
            _currentSelected = null;
        }
    }
}