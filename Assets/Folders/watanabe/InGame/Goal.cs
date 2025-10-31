using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    public GameObject resultPanel;　 //リザルトUIパネル
    public Text medalText;   　　　　//リザルトに表示されるメダル数
    public int currentStageNumber;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("ゴール到達");

            //ゴールしたらボールを止める
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;  //回転を止める
                rb.gravityScale = 0f;
            }

            //リザルトフェイズに変更
            if (GameManager.instance != null)
            {
                GameManager.instance.ToResult();
            }

            //メダル獲得条件が決まったらメダルに関するコードをif文で書く
            //メダル獲得と保存
            int currentMedal = PlayerPrefs.GetInt("MedalCount", 0);
            currentMedal += 1;
            PlayerPrefs.SetInt("MedalCount", currentMedal);　//メダル数をセーブ
            PlayerPrefs.Save();　　//実際にディスク（端末）に保存を確定させる命令（なくても保存されるkpとが多いが確実に保存）
            Debug.Log("メダル獲得。メダル数" + currentMedal);


            //リザルト画面表示
            if (resultPanel != null)
            {
                resultPanel.SetActive(true);
            }

            //リザルト画面に表示
            if (medalText != null)
            {
                medalText.text = "メダル：" + currentMedal;
            }

            // ステージクリア後に次ステージを解放
            StageSerect.UnlockNextStage(currentStageNumber);
        
        }
    }

    public void ResetMedal()
    {
        PlayerPrefs.DeleteKey("MedalCount");
        PlayerPrefs.Save();
        Debug.Log("メダルをリセットした" );
    }
}
