using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleOnClick : MonoBehaviour
{
    [SerializeField] string _nextSceneName = "SelectScene";

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (IsSceneInBuildSettings(_nextSceneName))
			{
				Debug.Log(_nextSceneName + " が見つかりました。シーンをロードします。");
				SceneManager.LoadScene(_nextSceneName);
			}
			else
			{
				Debug.LogError("エラー: シーン名 '" + _nextSceneName + "' はBuild Settingsに登録されていません。");
			}
		}
	}

	public bool IsSceneInBuildSettings(string sceneName)
	{
		int buildIndex = SceneUtility.GetBuildIndexByScenePath(sceneName);

		return buildIndex != -1;    // buildIndex が -1 でなければ、シーンは登録されている
	}
}
