using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

/// <summary>
/// メニューウィンドウの処理を記述
/// Backボタンについては、UnityEventで設定できるためメソッドなし
/// </summary>
public class MenuSwitchManager : MonoBehaviour
{
	[Header("セレクトシーンを追加")]
	[SerializeField] SceneAsset _selectScene;
	string _inGameSceneName;

	private void Start()
	{
		_inGameSceneName = SceneManager.GetActiveScene().name; // InGameの名前を取得
	}

	public void OnReset()
    {
        SceneLoader.Instance.FadeOutAndLoad(_inGameSceneName);
        // SceneManager.LoadScene(_inGameSceneName); // InGameシーンをロードする
	}

	public void OnSelectScene()
	{
		if (_selectScene)
		{
            SceneLoader.Instance.FadeOutAndLoad(_selectScene.name);
            // SceneManager.LoadScene(_selectScene.name);
		}
		else Debug.Log("Sceneが設定されていません");
	}
}
