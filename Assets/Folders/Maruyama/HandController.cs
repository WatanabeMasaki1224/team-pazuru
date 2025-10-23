using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 空のHandManagerを作成してアタッチ
/// Image UI をクリックするとインスペクターで設定した図形プレハブが生成
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

    // 引数なしメソッド
    public void SpawnCurrentShape()
    {
        if (_currentShape == null || spawnPoint == null) return;

        Instantiate(_currentShape.shapePrefab, spawnPoint.position, Quaternion.identity);
        Debug.Log($"{_currentShape.shapeName} を生成しました。");
        // 生成位置にすでに図形があれば生成しない仕様にする
    }
}
