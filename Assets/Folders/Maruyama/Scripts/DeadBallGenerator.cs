using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// DeadBall の自動生成を管理するコンポーネント。
/// 一定間隔で DeadBall を生成し、
/// GameOver パネルと連携して Dead スクリプトへ参照を渡す。
/// </summary>
public class DeadBallGenerator : MonoBehaviour
{
    [Header("生成設定")]
    [SerializeField] GameObject _deadBallPrefab;
    [SerializeField] float _generateInterval = 0.3f;

    [Header("ゲームオーバーUI参照")]
    [SerializeField] GameObject _gameOverPanel;
    Text _gameOverText;

    void Start()
    {
        InitializeGameOverPanel();
        StartCoroutine(GenerateDeadBallCoroutine());
    }

    /// <summary>
    /// GameOver パネルとテキスト参照を初期化する。
    /// </summary>
    void InitializeGameOverPanel()
    {
        if (_gameOverPanel != null)
        {
            _gameOverText = _gameOverPanel.GetComponentInChildren<Text>();
        }
        else
        {
            Debug.LogWarning("GameOverPanel がシーン上に見つかりません。");
        }
    }

    /// <summary>
    /// 一定間隔で DeadBall を生成し、Dead スクリプトにパネル参照を渡すコルーチン。
    /// ゲームオーバー（Result 状態）中は生成を一時停止する。
    /// </summary>
    IEnumerator GenerateDeadBallCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_generateInterval);

            if (GameManager.instance == null) continue;

            // --- ゲームオーバー状態では生成しない ---
            if (GameManager.instance.CurrentState == GameState.Result)
            {
                continue;
            }

            // --- 配置フェーズ以外なら生成 ---
            if (GameManager.instance.CurrentState != GameState.Placing)
            {
                if (_deadBallPrefab == null)
                {
                    Debug.LogError("DeadBallPrefab が設定されていません。Inspector から Prefab をアタッチしてください。");
                    yield break;
                }

                GameObject deadBall = Instantiate(_deadBallPrefab, transform.position, transform.rotation);
                Debug.Log($"DeadBall を生成しました。状態: {GameManager.instance.CurrentState}");

                // --- Dead スクリプトに UI 情報を渡す ---
                var deadScript = deadBall.GetComponent<Dead>();
                if (deadScript != null)
                {
                    deadScript._gameOverPanel = _gameOverPanel;
                    deadScript._gameOverText = _gameOverText;
                }
            }
        }
    }
}
