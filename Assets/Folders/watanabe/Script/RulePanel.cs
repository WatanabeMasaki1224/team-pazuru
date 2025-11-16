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
        currentPage = 0;
        pages[currentPage].SetActive(true);
    }

    public void CloseRulePanel()
    {
        rulePanel.SetActive(false);
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
