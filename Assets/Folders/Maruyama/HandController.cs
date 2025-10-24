using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 空のHandManagerを作成してアタッチ
/// Image UI をクリックするとインスペクターで設定した図形プレハブが生成
/// この処理は State が Placing のときだけ行われるようにする 
/// </summary>
public class HandController : MonoBehaviour
{
    [System.Serializable]
    public class HandShape
    {
        public string shapeName;       // 図形名前
        public GameObject shapePrefab; // 生成図形Prefab
        public Image imageUI;          // 手札UI
    }

    [SerializeField] HandShape[] shapes;
    [SerializeField] Transform spawnPoint; // 生成位置
    [SerializeField] Vector2 checkSize = new Vector2(1f, 1f); // 生成判定Boxサイズ

    // 現在選択中の図形
    HandShape _currentShape;

    void Start()
    {
        foreach (var shape in shapes)
        {
            if (shape.imageUI != null)
            {
                // OnClickEvent を取得または追加
                var clickComponent = shape.imageUI.GetComponent<OnClickEvent>();
                if (clickComponent == null)
                    clickComponent = shape.imageUI.gameObject.AddComponent<OnClickEvent>();

                // UnityEvent を作って SpawnCurrentShape を呼ぶように登録
                UnityEvent e = new UnityEvent();
                e.AddListener(() => { _currentShape = shape; SpawnCurrentShape(); });

                // OnClickEvent にセット
                clickComponent.SetEvent(e);
            }
        }
    }

    /// <summary>
    /// 現在選択中の図形を生成する
    /// </summary>
    public void SpawnCurrentShape()
    {
        if (_currentShape == null || spawnPoint == null) return;

        // 生成位置に判定用Boxを置いて重なりを確認
        Collider2D hit = Physics2D.OverlapBox(spawnPoint.position, checkSize, 0f);

        if (hit != null)
        {
            Debug.Log("生成位置に既存オブジェクトがあります");
            return;
        }

        // 問題なければ生成
        Instantiate(_currentShape.shapePrefab, spawnPoint.position, Quaternion.identity);
        Debug.Log($"{_currentShape.shapeName} を生成しました。");
    }

    // デバッグ用にSceneビューで判定Boxを可視化
    void OnDrawGizmosSelected()
    {
        if (spawnPoint != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(spawnPoint.position, checkSize);
        }
    }
}
