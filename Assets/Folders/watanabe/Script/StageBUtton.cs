using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageBUtton : MonoBehaviour
{
    public string stageNumber;  //ƒV[ƒ“–¼‚ğİ’è

    private void Start()
    {
        Button btn = GetComponent<Button>();
        
        if (btn != null)
        {
            btn.onClick.AddListener(OnStageSelected);
        }
    }

    void OnStageSelected()
    {
        SceneManager.LoadScene(stageNumber);
    }
}
