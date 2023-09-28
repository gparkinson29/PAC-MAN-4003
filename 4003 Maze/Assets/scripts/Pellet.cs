using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet: MonoBehaviour
{
    void OnTriggerEnter(Collider other) //determines what the pellet has collided with and determines appropriate response based on object
    {
        if (other.gameObject.tag=="Player")
        {
            this.gameObject.SetActive(false);
            other.gameObject.SendMessage("IncreaseTail", SendMessageOptions.DontRequireReceiver);
           
        }
    }
}
