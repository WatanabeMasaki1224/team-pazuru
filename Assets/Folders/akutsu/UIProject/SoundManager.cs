using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance;

	private void Awake()
	{
		// 1. 既にインスタンスが存在するかチェック
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
			Debug.LogWarning("SoundManagerが重複して生成されました。新しいインスタンスを破棄します。");
		}
	}
}