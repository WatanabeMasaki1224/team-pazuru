using UnityEngine;
using System;

public class InGamePauseManager : MonoBehaviour
{
	public static event Action OnPause;
	public static event Action OnResume;
	// bool _isPaused = false; // ゲームがポーズ中かどうかを追跡する変数
	// public bool Paused => _isPaused;

	public void Pause()
	{
		OnPause?.Invoke();
	}

	public void Resume()
	{
		OnResume?.Invoke();
	}
}
