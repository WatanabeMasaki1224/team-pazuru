using UnityEngine;
using System.Collections;

public class DeadBallGenerator : MonoBehaviour
{
    [SerializeField] GameObject deadBallPrefab;
    [SerializeField] float generateInterval = 0.5f;

    void Start()
    {
        StartCoroutine(GenerateDeadBallCoroutine());
    }

    /// <summary>
    /// 指定された間隔でDeadBallを繰り返し生成するコルーチン
    /// </summary>
    private IEnumerator GenerateDeadBallCoroutine()
    {
        while (true)
        {
            // 待機時間を確保
            yield return new WaitForSeconds(generateInterval);

            // GameManagerが存在し、かつ現在の状態が Placing ではない時のみ生成
            if (GameManager.instance != null && GameManager.instance.CurrentState != GameState.Placing)
            {
                if (deadBallPrefab != null)
                {
                    Instantiate(deadBallPrefab, transform.position, transform.rotation);
                    Debug.Log($"DeadBallを生成しました。状態: {GameManager.instance.CurrentState}");
                }
                else
                {
                    Debug.LogError("DeadBallPrefabが設定されていません。InspectorからPrefabをアタッチしてください。");
                    yield break;
                }
            }
            // else: Placing 状態の場合は、次のインターバルまで待機する
        }
    }
}