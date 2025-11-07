using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnStage : MonoBehaviour
{
    // 戻りたいステージ選択シーン名を設定
    public string stageSelectSceneName = "StageSelect";

    // ボタンの OnClick に割り当てる
    public void OnBackButton()
    {
        // シーンをロードしてステージ選択に戻る
        SceneManager.LoadScene(stageSelectSceneName);
    }
}
