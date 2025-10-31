using UnityEngine;
using UnityEngine.UI;

public class StageSerect : MonoBehaviour
{
    public Button[] StageButton;

    void Start()
    {
        int unlockedStage = PlayerPrefs.GetInt("UnlockedStage", 1);

        for (int i = 0; i < StageButton.Length; i++)
        {
            if(i < unlockedStage)
            {
                StageButton[i].interactable = true;
                StageButton[i].image.color = Color.white;
            }
            else
            {
                StageButton[i].interactable = false;
                StageButton[i].image.color = Color.black;
            }
        }
    }

    public static void UnlockNextStage(int clearedStage)
    {
        int unlockedStage = PlayerPrefs.GetInt("UnlockedStage", 1);

        if(clearedStage >=  unlockedStage)
        {
            PlayerPrefs.SetInt("UnlockedStage" , clearedStage + 1 );
            PlayerPrefs.Save();
            Debug.Log("ステージ" + (clearedStage + 1) + "が解放された");
        }
    }

    //ステージ解放状態をリセット
    public void ResetStageUnlock()
    {
        PlayerPrefs.DeleteKey("UnlockedStage");  
        PlayerPrefs.Save();
        Debug.Log("ステージ解放状態をリセットしました");
    }


}
