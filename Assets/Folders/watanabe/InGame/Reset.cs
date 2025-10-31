using UnityEngine;

public class Reset : MonoBehaviour
{
   public void OnClickResult()
    {
        if(GameManager.instance != null)
        {
            GameManager.instance.ResetGame();
        }
    }
}
