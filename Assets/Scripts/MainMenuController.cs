using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MainMenuController : MonoBehaviour
{
    public GameObject lifesPanel;
    public GameObject tokensPanel;
    public Text[] tokensTexts;
    public Text[] lifesTexts;
    public Image topBar;
    public Text claimTimeText;
    public GameObject lifesButton;

    AsyncOperation asyncLoad;

    
    void Start()
    {
        if (PlayerPrefs.GetInt("First") == 0)
        {
            PlayerPrefs.SetInt("First", 1);
            PlayerPrefs.SetInt("Lifes", 3);
        }

        foreach (var text in tokensTexts) 
        { 
            text.text = PlayerPrefs.GetInt("Tokens").ToString();   
        }
        foreach (var text in lifesTexts)
        {
            text.text = PlayerPrefs.GetInt("Lifes").ToString();
        }

        topBar.fillAmount = (float)PlayerPrefs.GetInt("Tokens") / 100;
        StartCoroutine(LoadYourAsyncScene());

        string lastTime = PlayerPrefs.GetString("LastClaimTime", "");
        DateTime lastClaimTime;

        if (!string.IsNullOrEmpty(lastTime))
        {
            lastClaimTime = DateTime.Parse(lastTime);
        }
        else
        {
            lastClaimTime = DateTime.MinValue;
        }

        if(DateTime.Today > lastClaimTime)
        {
            lifesButton.SetActive(true);
            claimTimeText.gameObject.SetActive(false);
        }
        else
        {
            lifesButton.SetActive(false);
            claimTimeText.gameObject.SetActive(true);
            claimTimeText.text = GetTimeToNextClaim();
        }
        StartCoroutine(SetTimeText());
    }

    private string GetTimeToNextClaim()
    {
        int hours = Mathf.FloorToInt((float)(DateTime.Today.AddDays(1) - DateTime.Now).TotalHours);
        int minutes = Mathf.FloorToInt((float)(DateTime.Today.AddDays(1) - DateTime.Now).TotalMinutes) % 60;
        int seconds = Mathf.FloorToInt((float)(DateTime.Today.AddDays(1) - DateTime.Now).TotalSeconds) % 60;
        return (hours + ":" + minutes + ":" + seconds);
    }

    public void OnClaimLifes()
    {
        PlayerPrefs.SetString("LastClaimTime", DateTime.Now.ToString());
        claimTimeText.text = GetTimeToNextClaim();
        lifesButton.SetActive(false);
        claimTimeText.gameObject.SetActive(true);

        PlayerPrefs.SetInt("Lifes", PlayerPrefs.GetInt("Lifes") + 3);
        foreach (var text in lifesTexts)
        {
            text.text = PlayerPrefs.GetInt("Lifes").ToString();
        }

        Debug.Log("APRETE");
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
        if (PlayerPrefs.GetInt("Lifes") > 0)
            asyncLoad.allowSceneActivation = true;
        else lifesPanel.SetActive(true);
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

    IEnumerator SetTimeText()
    {
        yield return new WaitForSeconds(1);
        claimTimeText.text = GetTimeToNextClaim();
        StartCoroutine(SetTimeText());
    }




    public void AddLifes()
    {
        PlayerPrefs.SetInt("Lifes", PlayerPrefs.GetInt("Lifes") + 1);
        foreach (var text in lifesTexts)
        {
            text.text = PlayerPrefs.GetInt("Lifes").ToString();
        }
    }
}
