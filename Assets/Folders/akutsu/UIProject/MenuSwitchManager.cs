using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// メニューウィンドウの処理を記述
/// Backボタンについては、UnityEventで設定できるためメソッドなし
/// </summary>
public class MenuSwitchManager : MonoBehaviour
{
    [Header("セレクトシーン名を追加")]
    [SerializeField] string _selectSceneName = "StageSelect";
    string _inGameSceneName;

    private void Start()
    {
        _inGameSceneName = SceneManager.GetActiveScene().name; // InGameの名前を取得
    }

    public void OnReset()
    {
        if (SceneLoader.Instance != null)
            SceneLoader.Instance.FadeOutAndLoad(_inGameSceneName);
        else
            SceneManager.LoadScene(_inGameSceneName); // InGameシーンをロードする
    }

    public void OnSelectScene()
    {
        if (!string.IsNullOrEmpty(_selectSceneName))
        {
            if (SceneLoader.Instance != null)
                SceneLoader.Instance.FadeOutAndLoad(_selectSceneName);
            else
                SceneManager.LoadScene(_selectSceneName);
        }
        else
        {
            Debug.Log("Scene名が設定されていません");
        }
    }
}
