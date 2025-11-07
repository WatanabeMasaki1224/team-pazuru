using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

/// <summary>
/// シーン遷移時に指定色のフェードイン・フェードアウトを行うシングルトン。
/// Panel(Image)の色をそのまま使う
/// </summary>
public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    [SerializeField] Image fadeImage;
    [SerializeField] float duration = 1.0f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        var c = fadeImage.color;
        fadeImage.color = new Color(c.r, c.g, c.b, 0f);
    }

    void Start()
    {
        FadeIn();
    }

    public void FadeOutAndLoad(string sceneName)
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.DOFade(1f, duration).SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                SceneManager.LoadScene(sceneName);
                FadeIn();
            });
    }

    public void FadeIn()
    {
        var c = fadeImage.color;
        fadeImage.color = new Color(c.r, c.g, c.b, 1f);

        fadeImage.DOFade(0f, duration).SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                fadeImage.gameObject.SetActive(false);
            });
    }
}
