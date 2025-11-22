using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    // BGM用かSE用か選べるように
    public enum SoundType { BGM, SE }
    public SoundType type;

    void Start()
    {
        Slider slider = GetComponent<Slider>();

        if (SoundManager.Instance == null) return;

        // タイプによって挙動を分ける
        if (type == SoundType.BGM)
        {
            // 1. スライダーの位置を今の音量に合わせる
            slider.value = SoundManager.Instance.GetBGMVolume();

            // 2. スライダーを動かした時にSoundManagerを呼ぶように登録
            slider.onValueChanged.AddListener((vol) => SoundManager.Instance.SetBGM(vol));
        }
        else
        {
            slider.value = SoundManager.Instance.GetSEVolume();
            slider.onValueChanged.AddListener((vol) => SoundManager.Instance.SetSE(vol));
        }
    }
}