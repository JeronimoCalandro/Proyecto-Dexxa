using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashController : MonoBehaviour
{
    AsyncOperation asyncLoad;
    void Start()
    {
        QualitySettings.SetQualityLevel(1, true);
        // Set Platform Settings to optimize for disk size (LTO)
        /*UnityEditor.EditorUserBuildSettings.SetPlatformSettings("Application.platform",
                                            "CodeOptimization",
                                            "disksizelto");*/

        //if (!isMobileCheck.CheckMobile()) Screen.SetResolution(1920, 1080, false);
        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    {
        asyncLoad = SceneManager.LoadSceneAsync(1);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void LoadMenu()
    {
        asyncLoad.allowSceneActivation = true;
        SoundController.instance.playMusic(SoundController.instance.GameMusic);
        SoundController.instance.playSound(SoundController.instance.button, false, SoundController.instance.fxAudioSource);
    }
}
