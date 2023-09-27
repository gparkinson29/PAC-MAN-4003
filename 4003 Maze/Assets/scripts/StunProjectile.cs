using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunProjectile : MonoBehaviour
{
    private int speed = 20;
    [SerializeField]
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = this.transform.forward * speed;

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag=="Walls")
        {
            Destroy(this.gameObject);
        }
    }

  
}
