using UnityEngine;

// �O�p�ɂ���K�v�ȃX�N���v�g���Ȃ����߁AMonoBehaviour���p�����Ă��܂��B
/// <summary>
/// �M�~�b�N���R�X�g���ɂȂ������ɎQ�Ƃ���N���X
/// </summary>
public class GimmikCost : MonoBehaviour
{
    [SerializeField] int _cost = 1;
    public int Cost => _cost;
}
