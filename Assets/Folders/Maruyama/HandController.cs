using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// ���HandManager���쐬���ăA�^�b�`
/// Image UI ���N���b�N����ƃC���X�y�N�^�[�Őݒ肵���}�`�v���n�u������
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

    // �����Ȃ����\�b�h
    public void SpawnCurrentShape()
    {
        if (_currentShape == null || spawnPoint == null) return;

        Instantiate(_currentShape.shapePrefab, spawnPoint.position, Quaternion.identity);
        Debug.Log($"{_currentShape.shapeName} �𐶐����܂����B");
        // �����ʒu�ɂ��łɐ}�`������ΐ������Ȃ��d�l�ɂ���
    }
}
