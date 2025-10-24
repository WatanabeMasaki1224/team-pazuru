using UnityEngine;

public enum GameState
{
    Placing,  //�}�`�z�u�t�F�C�Y
    Playing,�@//�Q�[���t�F�C�Y�i�X�^�[�g�������{�[�����������Ă���j
    Result,�@ //���U���g�t�F�C�Y
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
        Debug.Log("�}�`�z�u�t�F�C�Y��");
    }

    public void StartGame()
    {
        CurrentState = GameState.Playing;
        Debug.Log("�X�^�[�g");
    }

    public void ToResult()
    {
        CurrentState = GameState.Result;
        Debug.Log("���U���g�\��");
    }

    public void ResetGame()
    {
        CurrentState = GameState.Placing;
        Debug.Log("�z�u�t�F�C�Y�ɂ��ǂ�");

        //��ʏ�̃{�[�������ׂč폜
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }
    }
}
