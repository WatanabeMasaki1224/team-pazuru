using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// �Q�[���̏�Ԃɉ����� Physics2DRaycaster �������ŗL���E�����ɂ���B
/// �J�����ɃA�^�b�`���Ďg���B
/// </summary>
[RequireComponent(typeof(Physics2DRaycaster))]
public class CameraController : MonoBehaviour
{
    Physics2DRaycaster _raycaster;

    void Awake()
    {
        _raycaster = GetComponent<Physics2DRaycaster>();
    }

    void Update()
    {
        if (GameManager.instance == null) return;

        // Placing �t�F�C�Y�̂� Raycaster �L��
        _raycaster.enabled = (GameManager.instance.CurrentState == GameState.Placing);
    }
}
