using UnityEngine;

/// <summary>
/// 星のペアごとに空の親オブジェクトにセットで入れる
/// 星のワープ挙動とペアの位置を捉える処理を行う
/// </summary>
public class GimmikStarManager : MonoBehaviour
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

		Transform parentTransform = transform.parent; // 自身の親オブジェクトを取得

		if (parentTransform)
		{
			_pairStarPos = parentTransform.Find(_partner.name).position;	// ペアとなる星オブジェクトの位置を特定

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
		if(collision.tag == "Ball"　&& _parentManager.IsWarp) // 親オブジェクトのスクリプトからワープできるか確認
		{
			collision.transform.position = _pairStarPos;

			_parentManager.WarpRestriction(); // 連続ワープにならないように制限をかける
		}
	}
}
