using UnityEngine;
using UnityEngine.UI;

public class Dead : MonoBehaviour
{
    public GameObject _gameOverPanel;
    public Text _gameOverText;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("ゲームオーバー");

            // ボールを止める
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.gravityScale = 0f;
            }

            //リザルトフェイズに変更
            if (GameManager.instance != null)
            {
                GameManager.instance.ToResult();
            }

            //画面表示
            if (_gameOverPanel != null)
            {
                _gameOverPanel.SetActive(true);
            }

            //リザルト画面に表示
            if (_gameOverText != null)
            {
                // _gameOverText.text = "ゲームオーバー";
            }
        }

        if (this.gameObject.tag == "Ball")
        Destroy(this.gameObject);
    }
}
