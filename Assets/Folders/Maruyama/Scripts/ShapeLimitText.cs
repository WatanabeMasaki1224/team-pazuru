using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShapeLimitText : MonoBehaviour
{
    [SerializeField] Text moveCountText;
    [SerializeField] TMP_Text _moveCountText;

    void OnEnable()
    {
        ButtonInteraction.OnStageHovered += UpdateMoveCount;
    }

    void OnDisable()
    {
        ButtonInteraction.OnStageHovered -= UpdateMoveCount;
    }

    void UpdateMoveCount(int stageNumber, int moveCount)
    {
        if (stageNumber == -1)
        {
            moveCountText.text = "メダル条件：?手以内";
            _moveCountText.text = "メダル条件：?手以内";
        }
        else
        {
            moveCountText.text = $"メダル条件：{moveCount}手以内";
            _moveCountText.text = $"メダル条件：{moveCount}手以内";
        }
    }
}
