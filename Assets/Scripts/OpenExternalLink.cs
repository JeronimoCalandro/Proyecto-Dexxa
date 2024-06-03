using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class OpenExternalLink : MonoBehaviour
{
    public void OpenLink(string _url)
    {
        openWindow(_url);
    }

    [DllImport("__Internal")]
    private static extern void openWindow(string url);
}
