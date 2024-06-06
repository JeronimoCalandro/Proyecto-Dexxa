using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class IsMobileCheck : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern bool IsMobile();

    public bool CheckMobile()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        return IsMobile();
#endif
        return false;
    }
}
