using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject lifesPanel;
    public GameObject tokensPanel;
    public Text[] tokensTexts;
    public Image topBar;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var text in tokensTexts) 
        { 
            text.text = PlayerPrefs.GetInt("Tokens").ToString();   
        }

        topBar.fillAmount = (float)PlayerPrefs.GetInt("Tokens") / 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(2);
    }

    public void OpenLifesPanel()
    {
        lifesPanel.SetActive(true);
    }

    public void OpenTokensPanel()
    {
        tokensPanel.SetActive(true);
    }

    public void ClosePanels()
    {
        lifesPanel.SetActive(false);
        tokensPanel.SetActive(false);
    }
}
