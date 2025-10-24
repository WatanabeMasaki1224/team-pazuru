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
    // public bool _isSpawned = false;

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

        // �����ʒu�ɑ��̃I�u�W�F�N�g�����邩�`�F�b�N
        Collider2D hit = Physics2D.OverlapBox(spawnPoint.position,
            _currentShape.shapePrefab.GetComponent<Collider2D>().bounds.size, 0f);

        if (hit != null)
        {
            Debug.Log("�����ʒu�Ɋ����I�u�W�F�N�g������܂�");
            return;
        }

        // ���Ȃ���ΐ���
        Instantiate(_currentShape.shapePrefab, spawnPoint.position, Quaternion.identity);
        // _isSpawned = true;
        Debug.Log($"{_currentShape.shapeName} �𐶐����܂����B");
    }
}
