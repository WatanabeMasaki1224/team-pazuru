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
		if (SceneLoader.Instance != null)
			SceneLoader.Instance.FadeOutAndLoad(_inGameSceneName);
        else
			SceneManager.LoadScene(_inGameSceneName); // InGameシーンをロードする
	}

	public void OnSelectScene()
	{
		if (_selectScene)
		{
            if (SceneLoader.Instance != null)
                SceneLoader.Instance.FadeOutAndLoad(_selectScene.name);
            else
				SceneManager.LoadScene(_selectScene.name);
		}
		else Debug.Log("Sceneが設定されていません");
	}
}
