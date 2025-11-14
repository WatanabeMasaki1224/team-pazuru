using TMPro;
using UnityEngine;

public class RulePanel : MonoBehaviour
{
    [Header("UIéQè∆")]
    public GameObject rulePanel;
    public GameObject[] pages;
    private int currentPage = 0;

    void Start()
    {
        rulePanel.SetActive(false);
    }

    public void OpenRulePanel()
    {
        rulePanel.SetActive(true);
        currentPage = 0;
        ShowPage(currentPage);
    }

    public void CloseRulePanel()
    {
        rulePanel.SetActive(false);
    }

    public void NextPage()
    {
        if (currentPage < pages.Length - 1)
        {
            currentPage++;
            ShowPage(currentPage);
        }
    }

    public void PrevPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            ShowPage(currentPage);
        }
    }

    private void ShowPage(int index)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == index);
        }
    }
}
