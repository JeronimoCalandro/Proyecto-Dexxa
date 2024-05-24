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

    AsyncOperation asyncLoad;
    
    void Start()
    {
        foreach (var text in tokensTexts) 
        { 
            text.text = PlayerPrefs.GetInt("Tokens").ToString();   
        }

        topBar.fillAmount = (float)PlayerPrefs.GetInt("Tokens") / 100;
        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    {
        asyncLoad = SceneManager.LoadSceneAsync(2);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void LoadGameScene()
    {
        asyncLoad.allowSceneActivation = true;
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
