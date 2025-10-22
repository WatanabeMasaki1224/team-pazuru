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
            Debug.Log("�{�[����Prefab���ݒ肳��Ă��܂���");
            return;
        }
        
        if ( spawnPoint == null ) 
        {
            Debug.Log("�X�|�[���|�C���g��Prefab���ݒ肳��Ă��܂���");
            return;
        }

        if( currentBall != null )
        {
            Debug.Log("���łɃ{�[�������݂��Ă��܂�");
            return;
        }

        currentBall =Instantiate(ballPrefab, spawnPoint);
    }
}
