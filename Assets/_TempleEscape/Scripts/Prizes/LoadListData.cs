using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadListData : MonoBehaviour
{
    public GameObject Name;
    public GameObject ParticipantsList;
    private List<Dictionary<string, object>> data;
    private void Awake()
    {
        InitializeList();
    }

    private void InitializeList()
    {

        
    }
}
