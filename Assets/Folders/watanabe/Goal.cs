using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject resultPanel;

    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.CompareTag("Ball"))
        {
            Debug.Log("ƒS[ƒ‹“’B");
            
            if(resultPanel != null)
            {
                resultPanel.SetActive(true);
            }
        }
    }
}
