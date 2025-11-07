using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using NUnit.Framework.Constraints;

using Unity.VisualScripting;

public class Goal : MonoBehaviour
{
    [Header("UI関係")]
    public GameObject resultPanel;　 //リザルトUIパネル
    public Text clearText;
    public GameObject buttonGroup;

    [Header("ステージ情報")]
    public int currentStageNumber; //ステージの番号
    public int shapeLimit = 5;   //メダル獲得のための図形配置上限

    [Header("メダルUI")]
    public Image blackMedal;
    public Image yellowMedal;
    public Image newMedal;

    private bool acquired;　//メダルを獲得済みか
    private bool medalJudgment;　　//メダル獲得判定
    bool isNewMedal; //新しくメダルを獲得したか

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


            //図形の数をカウント
            GameObject[] shapes = GameObject.FindGameObjectsWithTag("Shape");
            int shapeCount = shapes.Length;
            Debug.Log($"図形配置数:{shapeCount}/図形配置上限:{shapeLimit}");

            //メダル獲得判定（上限以下ならtrue）
            medalJudgment = shapeCount <= shapeLimit;

            //ステージ固有の保存キー
            string stageMedalKey = $"Medal_Stage_{currentStageNumber}";

            //メダル獲得済みかチェック
            acquired = PlayerPrefs.GetInt(stageMedalKey, 0) == 1;

            //メダルの総数を読み込み
            int totalMedal = PlayerPrefs.GetInt("MedalCount", 0);

            if (medalJudgment && !acquired)
            {
                totalMedal += 1;
                PlayerPrefs.SetInt("MedalCount" , totalMedal);
                PlayerPrefs.SetInt(stageMedalKey , 1);
                PlayerPrefs.Save();
                acquired = true;
                isNewMedal = true;
                Debug.Log($"メダル獲得！（ステージ{currentStageNumber}） 合計{totalMedal}");
            }

            else if(acquired)
            {
                Debug.Log($"メダル獲得済");
            }

            else
            {
                Debug.Log("メダル獲得条件を満たしてない");
            }

            //リザルト画面表示
            if (resultPanel != null)
            {
                resultPanel.SetActive(true);
            }

            if (buttonGroup != null)
            {
                buttonGroup.SetActive(false);
            }

            if (clearText != null)
            {
                clearText.gameObject.SetActive(true);
                StartCoroutine(ClearAnimation());
            }

            // ステージクリア後に次ステージを解放
            StageSerect.UnlockNextStage(currentStageNumber);
        
        }
    }

    public void ResetMedal()
    {
        PlayerPrefs.DeleteKey("MedalCount");
        //ステージごとの獲得状況も削除
        for (int i = 1; i <= 8; i++) // 最大100ステージ想定
        {
            string key = $"Medal_Stage_{i}";
            if (PlayerPrefs.HasKey(key))
                PlayerPrefs.DeleteKey(key);
        }
    　　
        PlayerPrefs.Save();
        Debug.Log("メダルをリセットした" );
    }

    private IEnumerator ClearAnimation()
    {
        //元のスケールを記憶
        Vector3 originalScale = clearText.transform.localScale;
        //最初は小さく
        clearText.transform.localScale = Vector3.zero;
        float duration = 1.0f; //拡大にかかる時間
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float scale = Mathf.Lerp(0f,1f,time/duration);
            clearText.transform.localScale = originalScale * scale;
            yield return null;
        }

        clearText.transform.localScale = originalScale;

        if(buttonGroup != null)
        {
            buttonGroup.SetActive(true);
        }

        if(blackMedal != null)
        {
            blackMedal.gameObject.SetActive(!acquired); 
        }

        if(yellowMedal != null)
        {
            yellowMedal.gameObject.SetActive(acquired);
        }

        if(newMedal != null && medalJudgment && isNewMedal)
        {
            StartCoroutine(MedalAnimation());
        }
    }

    private IEnumerator MedalAnimation()
    {
        //最初は大きく表示
        newMedal.transform.localScale = Vector3.one * 3;
        newMedal.gameObject.SetActive(true) ;
        blackMedal.gameObject.SetActive(true);  //背景用

        Vector3 targetScale = blackMedal.transform.localScale;
        float duration = 1.0f;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            newMedal.transform.localScale = Vector3.Lerp(Vector3.one * 5 , targetScale, time/duration);
            yield return null;
        }

        newMedal.transform.position = targetScale; 
    }
}
