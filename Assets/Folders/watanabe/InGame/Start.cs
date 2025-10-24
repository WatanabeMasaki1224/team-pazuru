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
            Debug.Log("�z�u�t�F�C�Y�̂݃X�^�[�g�{�^���������܂�");
            return;
        }
       
        if (ballPrefab == null )
        {
            Debug.Log("�{�[����Prefab���ݒ肳��Ă��܂���");
            return;
        }
        
        if (spawnPoint == null ) 
        {
            Debug.Log("�X�|�[���|�C���g��Prefab���ݒ肳��Ă��܂���");
            return;
        }

        if( currentBall != null )
        {
            Debug.Log("���łɃ{�[�������݂��Ă��܂�");
            return;
        }
        GameManager.instance.StartGame();  //�Q�[���t�F�C�Y�ɏ�ԕύX
        currentBall =Instantiate(ballPrefab, spawnPoint);
    }
}
