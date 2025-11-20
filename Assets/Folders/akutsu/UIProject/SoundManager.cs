using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] AudioMixer audioMixer; // インスペクターでセットする

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // スライダーからこの関数を呼んでもらう
    public void SetBGM(float volume)
    {
        audioMixer.SetFloat("BGM", volume);
    }

    public void SetSE(float volume)
    {
        audioMixer.SetFloat("SE", volume);
    }

    // スライダーの初期位置を決めるために今の音量を取得する
    public float GetBGMVolume()
    {
        audioMixer.GetFloat("BGM", out float vol);
        return vol;
    }

    public float GetSEVolume()
    {
        audioMixer.GetFloat("SE", out float vol);
        return vol;
    }
}