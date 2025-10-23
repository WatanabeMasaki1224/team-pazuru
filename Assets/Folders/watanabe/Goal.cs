using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    public GameObject resultPanel;�@ //���U���gUI�p�l��
    public Text medalText;   �@�@�@�@//���U���g�ɕ\������郁�_����

    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.CompareTag("Ball"))
        {
            Debug.Log("�S�[�����B");

            //�S�[��������{�[�����~�߂�
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.gravityScale = 0f;
            }

            //���U���g�t�F�C�Y�ɕύX
            if(GameManager.instance != null)
            {
                GameManager.instance.ToResult();
            }

            //���_���l�����������܂����烁�_���Ɋւ���R�[�h��if���ŏ���
            //���_���l���ƕۑ�
            int currentMedal = PlayerPrefs.GetInt("MedalCount", 0);
            currentMedal += 1;
            PlayerPrefs.SetInt("MedalCount",currentMedal);
            PlayerPrefs.Save();
            Debug.Log("���_���l���B���_����" + currentMedal);


            //���U���g��ʕ\��
            if (resultPanel != null)
            {
                resultPanel.SetActive(true);
            }

            //���U���g��ʂɕ\��
            if (medalText != null)
            {
                medalText.text = "���_���F" + currentMedal;
            }
        }
    }

    public void ResetMedal()
    {
        PlayerPrefs.DeleteKey("MedalCount");
        PlayerPrefs.Save();
        Debug.Log("���_�������Z�b�g����" );
    }
}
