using UnityEngine;

/// <summary>
/// 星のペアごとに空の親オブジェクトにセットで入れる
/// 星のワープ挙動とペアの位置を捉える処理を行う
/// </summary>
public class GimmikStarManager_Modified : MonoBehaviour
{
    [SerializeField] GameObject _partner;
    Vector2 _pairStarPos;
    StarParentManager _parentManager;

    void Start()
    {
        _parentManager = GetComponentInParent<StarParentManager>();
        if (_parentManager == null)
        {
            Debug.LogError("親オブジェクトのスクリプトが見つかりません。");
            return;
        }

        _pairStarPos = _partner.transform.localPosition;

        if (_pairStarPos != null)
        {
            Debug.Log($"{transform.name}のペア（{_pairStarPos}）の位置を見つけました: {_pairStarPos}");
        }
        else
        {
            Debug.LogError($"ペアとなるオブジェクトが見つかりません: {_pairStarPos}");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_partner == null || _parentManager == null) return;
        if (collision.CompareTag("Ball") && _parentManager.IsWarp)
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            Vector2 tempVelocity = Vector2.zero;

            if (rb != null)
            {
                tempVelocity = rb.linearVelocity; // 速度を保存
            }

            // ワールド座標でワープ
            collision.transform.position = _partner.transform.position;

            // 保存した速度を代入
            if (rb != null)
            {
                rb.linearVelocity = tempVelocity;
            }

            // 連続ワープ防止
            _parentManager.WarpRestriction();
        }
    }

}
