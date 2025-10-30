using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
	[SerializeField] AudioMixer _audioMixer;

	[SerializeField] Slider _bgmSlider;
	[SerializeField] Slider _seSlider;

	private void Start()
	{
		_audioMixer.GetFloat("BGM", out float bgmVolume);
		_bgmSlider.value = bgmVolume;

		_audioMixer.GetFloat("SE", out float seVolume);
		_seSlider.value = seVolume;
	}

	public void SetBGM(float volume)
	{
		_audioMixer.SetFloat("BGM", volume);
	}

	public void SetSE(float volume)
	{
		_audioMixer.SetFloat("SE", volume);
	}
}