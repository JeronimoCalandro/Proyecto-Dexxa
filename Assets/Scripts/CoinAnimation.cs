using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieDriveGame;

public class CoinAnimation : MonoBehaviour
{
    public string ui;
    Transform UICoin;
    void Start()
    {
        UICoin = GameObject.FindGameObjectWithTag(ui).transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3( UICoin.position.x, UICoin.position.y, UICoin.position.z + 3), 5f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Debug.Log("ENTRO");
            if (ui == "Life")
                ZDGGameController.instance.ChangeHealth(1);
            else
                ZDGGameController.instance.ChangeScore(1);
            //GameplayController.instance.DisplayScore();
            Destroy(gameObject);
        }
    }
}
