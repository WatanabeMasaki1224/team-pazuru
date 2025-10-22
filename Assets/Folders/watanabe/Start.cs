using UnityEngine;
using UnityEngine.UI;

public class Start : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform spawnPoint;
    private GameObject currentBall;

    public void StartButton()
    {
        if (ballPrefab == null )
        {
            Debug.Log("ボールのPrefabが設定されていません");
            return;
        }
        
        if ( spawnPoint == null ) 
        {
            Debug.Log("スポーンポイントのPrefabが設定されていません");
            return;
        }

        if( currentBall != null )
        {
            Debug.Log("すでにボールが存在しています");
            return;
        }

        currentBall =Instantiate(ballPrefab, spawnPoint);
    }
}
