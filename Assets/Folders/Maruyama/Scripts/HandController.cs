using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 空のHandManagerを作成してアタッチ
/// UIオブジェクトをクリックするとインスペクターで設定した図形プレハブが生成
/// この処理は State が Placing のときだけ行われるようにする 
/// </summary>
public class HandController : MonoBehaviour
{
    [System.Serializable]
    public class HandShape
    {
        [Tooltip("図形の名前")]
        public string shapeName;

        [Tooltip("生成する図形のPrefab")]
        public GameObject shapePrefab;

        [Tooltip("手札UIに対応するUIオブジェクト（ボタンなど）")]
        public GameObject uiObject;
    }

    [SerializeField, Tooltip("手札の配列。Inspectorで設定する")]
    HandShape[] shapes;

    [SerializeField, Tooltip("図形を生成する位置")]
    Transform spawnPoint;

    [SerializeField, Tooltip("生成判定用のBoxサイズ")]
    Vector2 checkSize = new Vector2(1f, 1f);

    // 現在選択中の図形
    HandShape _currentShape;

    void Start()
    {
        foreach (var shape in shapes)
        {
            if (shape.uiObject != null)
            {
                // OnClickEvent コンポーネントを取得または追加
                var clickComponent = shape.uiObject.GetComponent<OnClickEvent>();
                if (clickComponent == null)
                    clickComponent = shape.uiObject.AddComponent<OnClickEvent>();

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
        if (GameManager.instance.CurrentState != GameState.Placing) return;
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
