using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// シーン遷移時に中心から広がる・閉じるフェード演出を行うシングルトン。
/// DontDestroyOnLoadでシーンを跨いでも破棄されない。
/// </summary>
public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    [SerializeField] Image fadeImage;
    [SerializeField] float duration = 0.8f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // 起動時はフェードイン
        FadeIn();
    }

    public void FadeOutAndLoad(string sceneName)
    {
        fadeImage.transform.localScale = Vector3.zero;
        fadeImage.gameObject.SetActive(true);
        fadeImage.transform
            .DOScale(Vector3.one * 2f, duration)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                SceneManager.LoadScene(sceneName);
                FadeIn();
            });
    }

    public void FadeIn()
    {
        fadeImage.transform.localScale = Vector3.one * 2f;
        fadeImage.gameObject.SetActive(true);
        fadeImage.transform
            .DOScale(Vector3.zero, duration)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                fadeImage.gameObject.SetActive(false);
            });
    }
}
