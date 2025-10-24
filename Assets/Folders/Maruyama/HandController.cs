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
        public string shapeName;       // �}�`���O
        public GameObject shapePrefab; // �����}�`Prefab
        public Image imageUI;          // ��DUI
    }

    [SerializeField] HandShape[] shapes;
    [SerializeField] Transform spawnPoint; // �����ʒu
    [SerializeField] Vector2 checkSize = new Vector2(1f, 1f); // ��������Box�T�C�Y

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
