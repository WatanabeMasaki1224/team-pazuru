using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class Dead : MonoBehaviour
{
    public GameObject _gameOverPanel;
    public Text _gameOverText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("�Q�[���I�[�o�[");

            // �{�[�����~�߂�
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.gravityScale = 0f;
            }

            //���U���g�t�F�C�Y�ɕύX
            if (GameManager.instance != null)
            {
                GameManager.instance.ToResult();
            }

            //��ʕ\��
            if (_gameOverPanel != null)
            {
                _gameOverPanel.SetActive(true);
            }

            //���U���g��ʂɕ\��
            if (_gameOverText != null)
            {
                _gameOverText.text = "�Q�[���I�[�o�[";
            }
        }
    }
}
