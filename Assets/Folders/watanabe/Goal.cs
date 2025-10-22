using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject resultPanel;

    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.CompareTag("Ball"))
        {
            Debug.Log("�S�[�����B");
            
            if(resultPanel != null)
            {
                resultPanel.SetActive(true);
            }
        }
    }
}
