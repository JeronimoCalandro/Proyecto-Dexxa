using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashController : MonoBehaviour
{
    public void LoadMenuScene()
    {
        SceneManager.LoadScene(1);
    }
}
