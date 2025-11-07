using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 各ステージボタンを管理し、
/// 押下時に指定されたシーンをロードする。
/// </summary>
[RequireComponent(typeof(Button))]
public class StageButton : MonoBehaviour
{
    [SerializeField] string sceneName;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnStageSelected);
    }

    void OnStageSelected()
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogWarning($"{name} にシーン名が設定されていません。");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }
}
