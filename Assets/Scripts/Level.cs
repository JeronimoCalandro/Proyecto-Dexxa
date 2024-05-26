using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using ZombieDriveGame;

public class Level : MonoBehaviour
{
    private void Reset()
    {
        
    }

    public static void TurnOn(Level b)
    {
        b.Reset();
        b.gameObject.SetActive(true);
    }

    public static void TurnOff(Level b)
    {
        b.gameObject.SetActive(false);
    }
}
