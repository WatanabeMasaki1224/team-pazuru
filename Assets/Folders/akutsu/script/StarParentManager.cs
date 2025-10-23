using System.Collections;
using UnityEngine;

public class StarParentManager : MonoBehaviour
{
    bool _isWarp = true;
    bool _isCoolDown = false;
    float _timer;
    [SerializeField] float _restrictionTime = 2f;

    public bool IsWarp => _isWarp;

    /// <summary>
    /// 星のワープが機能したときに呼び出す関数
    /// ワープの処理を制限する
    /// </summary>
    public void WarpRestriction()
    {
        if (!_isCoolDown)
        {
            _isWarp = false;
            StartCoroutine(CooldownCoroutine());
        }
    }

	IEnumerator CooldownCoroutine()
	{
        _isCoolDown = true;

		yield return new WaitForSeconds(_restrictionTime);

        _isCoolDown = false;

		Debug.Log("ワープシステム：クールダウン終了");

		_isWarp = true;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Ball")
        {
            // ここ機能しないよね？
        }
	}
}
