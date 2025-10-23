using UnityEngine;

/// <summary>
/// 星のペアごとに空の親オブジェクトにセットで入れる
/// 星のワープ挙動とペアの位置を捉える処理を行う
/// </summary>
public class GimmikStarManager : MonoBehaviour
{
    [SerializeField] string _pairStarName = default;
	Vector2 _pairStarPos;

    void Start()
    {
		Transform parentTransform = transform.parent; // 自身の親オブジェクトを取得

		if (parentTransform)
		{
			_pairStarPos = parentTransform.Find(_pairStarName).position;	// ペアとなる星オブジェクトの位置を特定

			if (_pairStarPos != null)
			{
				Debug.Log($"{transform.name}のペア（{_pairStarPos}）の位置を見つけました: {_pairStarPos}");
			}
			else
			{
				Debug.LogError($"ペアとなるオブジェクトが見つかりません: {_pairStarPos}");
			}
		}
		else
		{
			Debug.LogError("このワープポイントには親オブジェクトがありません。");
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Ball")
		{
			collision.transform.position = _pairStarPos;
		}
	}
}
