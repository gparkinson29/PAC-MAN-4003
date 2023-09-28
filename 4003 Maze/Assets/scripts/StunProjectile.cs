using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunProjectile : MonoBehaviour
{
    private int speed = 20;
    [SerializeField]
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start() //initializes variable and sets its trajectory
    {
        rb.velocity = this.transform.forward * speed;

    }

    void OnCollisionEnter(Collision other) //checks what the projectile has collided with and determines response accordingly
    {
        if (other.gameObject.tag=="Walls")
        {
            Destroy(this.gameObject);
        }
    }

  
}
