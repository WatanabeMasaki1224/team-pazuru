using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnStage : MonoBehaviour
{
    // �߂肽���X�e�[�W�I���V�[������ݒ�
    public string stageSelectSceneName = "StageSelect";

    // �{�^���� OnClick �Ɋ��蓖�Ă�
    public void OnBackButton()
    {
        // �V�[�������[�h���ăX�e�[�W�I���ɖ߂�
        SceneManager.LoadScene(stageSelectSceneName);
    }
}
