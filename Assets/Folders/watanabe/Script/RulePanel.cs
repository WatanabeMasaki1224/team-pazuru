using TMPro;
using UnityEngine;

public class RulePanel : MonoBehaviour
{
    [SerializeField] GameObject rulePanel;
    [SerializeField] GameObject[] pages;
    int currentPage = 0;

    void Start()
    {
        rulePanel.SetActive(false);
    }

    public void OpenRulePanel()
    {
        rulePanel.SetActive(true);

        // すべて非表示
        foreach (var page in pages)
        {
            page.SetActive(false);
        }

        currentPage = 0;
        pages[currentPage].SetActive(true);
    }

    public void CloseRulePanel()
    {
        rulePanel.SetActive(false);

        // すべてのページを非表示にする
        foreach (var page in pages)
        {
            page.SetActive(false);
        }
    }

    public void NextPage()
    {
        if (currentPage < pages.Length - 1)
        {
            pages[currentPage].SetActive(false); 
            currentPage++;                        
            pages[currentPage].SetActive(true);   
        }
    }

    public void PrevPage()
    {
        if (currentPage > 0)
        {
            pages[currentPage].SetActive(false);  
            currentPage--;                        
            pages[currentPage].SetActive(true);   
        }
    }
}
