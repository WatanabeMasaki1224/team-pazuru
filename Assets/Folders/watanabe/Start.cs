using UnityEngine;
using UnityEngine.UI;

public class Start : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform spawnPoint;
    private GameObject currentBall;

    public void OnClickStart()
    {
        if(GameManager.instance.CurrentState != GameState.Placing)
        {
            Debug.Log("配置フェイズのみスタートボタンをおせます");
            return;
        }
       
        if (ballPrefab == null )
        {
            Debug.Log("ボールのPrefabが設定されていません");
            return;
        }
        
        if (spawnPoint == null ) 
        {
            Debug.Log("スポーンポイントのPrefabが設定されていません");
            return;
        }

        if( currentBall != null )
        {
            Debug.Log("すでにボールが存在しています");
            return;
        }
        GameManager.instance.StartGame();  //ゲームフェイズに状態変更
        currentBall =Instantiate(ballPrefab, spawnPoint);
    }
}
