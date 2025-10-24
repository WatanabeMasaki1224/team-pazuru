using UnityEngine;

public enum GameState
{
    Placing,  //図形配置フェイズ
    Playing,　//ゲームフェイズ（スタートを押しボールが落下してくる）
    Result,　 //リザルトフェイズ
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState CurrentState {  get; private set; } = GameState.Placing;
   
    public  void Awake()
    {
        instance = this;    
    }

    public void PlaceTime()
    {
        CurrentState = GameState.Placing;
        Debug.Log("図形配置フェイズへ");
    }

    public void StartGame()
    {
        CurrentState = GameState.Playing;
        Debug.Log("スタート");
    }

    public void ToResult()
    {
        CurrentState = GameState.Result;
        Debug.Log("リザルト表示");
    }

    public void ResetGame()
    {
        CurrentState = GameState.Placing;
        Debug.Log("配置フェイズにもどる");

        //画面上のボールをすべて削除
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }
    }
}
