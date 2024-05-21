using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bitel.Jetpack.Game
{
    public class Rotator : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.RotateAround(transform.position, transform.forward, Time.deltaTime * 90f);

        }
    }
}