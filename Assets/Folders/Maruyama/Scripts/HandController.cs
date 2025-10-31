using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// ���HandManager���쐬���ăA�^�b�`
/// Image UI ���N���b�N����ƃC���X�y�N�^�[�Őݒ肵���}�`�v���n�u������
/// ���̏����� State �� Placing �̂Ƃ������s����悤�ɂ��� 
/// </summary>
public class HandController : MonoBehaviour
{
    [System.Serializable]
    public class HandShape
    {
        [Tooltip("�}�`�̖��O")]
        public string shapeName;

        [Tooltip("��������}�`��Prefab")]
        public GameObject shapePrefab;

        [Tooltip("��DUI�ɑΉ�����Image")]
        public Image imageUI;
    }

    [SerializeField, Tooltip("��D�̔z��BInspector�Őݒ肷��")]
    HandShape[] shapes;

    [SerializeField, Tooltip("�}�`�𐶐�����ʒu")]
    Transform spawnPoint;

    [SerializeField, Tooltip("��������p��Box�T�C�Y")]
    Vector2 checkSize = new Vector2(1f, 1f);

    // ���ݑI�𒆂̐}�`
    HandShape _currentShape;


    void Start()
    {
        foreach (var shape in shapes)
        {
            if (shape.imageUI != null)
            {
                // OnClickEvent ���擾�܂��͒ǉ�
                var clickComponent = shape.imageUI.GetComponent<OnClickEvent>();
                if (clickComponent == null)
                    clickComponent = shape.imageUI.gameObject.AddComponent<OnClickEvent>();

                // UnityEvent ������� SpawnCurrentShape ���ĂԂ悤�ɓo�^
                UnityEvent e = new UnityEvent();
                e.AddListener(() => { _currentShape = shape; SpawnCurrentShape(); });

                // OnClickEvent �ɃZ�b�g
                clickComponent.SetEvent(e);
            }
        }
    }

    /// <summary>
    /// ���ݑI�𒆂̐}�`�𐶐�����
    /// </summary>
    public void SpawnCurrentShape()
    {
        if (GameManager.instance.CurrentState != GameState.Placing) return;
        if (_currentShape == null || spawnPoint == null) return;

        // �����ʒu�ɔ���pBox��u���ďd�Ȃ���m�F
        Collider2D hit = Physics2D.OverlapBox(spawnPoint.position, checkSize, 0f);

        if (hit != null)
        {
            Debug.Log("�����ʒu�Ɋ����I�u�W�F�N�g������܂�");
            return;
        }

        // ���Ȃ���ΐ���
        Instantiate(_currentShape.shapePrefab, spawnPoint.position, Quaternion.identity);
        Debug.Log($"{_currentShape.shapeName} �𐶐����܂����B");
    }

    // �f�o�b�O�p��Scene�r���[�Ŕ���Box������
    void OnDrawGizmosSelected()
    {
        if (spawnPoint != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(spawnPoint.position, checkSize);
        }
    }
}
