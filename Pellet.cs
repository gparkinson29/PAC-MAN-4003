using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet: MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            this.gameObject.SetActive(false);
            other.gameObject.SendMessage("IncreaseTail", SendMessageOptions.DontRequireReceiver);
           
        }
    }
}
