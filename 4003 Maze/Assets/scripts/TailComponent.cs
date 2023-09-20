using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TailComponent : MonoBehaviour
{
    [SerializeField]
    private int value;
    [SerializeField]
    private CharacterController cc;
    public GameManager info;

    void Start()
    {
        value = 1;
        info = Camera.main.GetComponent<GameManager>(); 
    }

    public CharacterController GetCharacterController()
    {
        return cc;
    }

    public int GetComponentValue()
    {
        return value;
    }

    public void RaiseValue(int amountToRaise)
    {
        value += amountToRaise;
        ReskinComponent();
    }

    public void LowerValue(int amountToLower)
    {
        value -= amountToLower;
    }

    public void ReskinComponent()
    {
        if (value == 1)
        {

        }
        else if (value == 2)
        {

        }
        else if (value == 3)
        {

        }
        else if (value == 4)
        {

        }
        else
        {

        }
    }

    public void OnTriggerEnter(Collider cos)
    {
        if (cos.gameObject.tag.Equals("Player"))
        {
            Debug.Log("you boned your tail");
            info.isHitFront = true;
            info.EndGame(); 
        }
        else if (cos.gameObject.tag.Equals("enemy1")) 
        {
            Debug.Log("boaner1");
        }
        else if (cos.gameObject.tag.Equals("enemy2")) 
        {
            Debug.Log("boaner2");
        }
        else if (cos.gameObject.tag.Equals("enemy3")) 
        {
            Debug.Log("boaner3");
        }
        else if (cos.gameObject.tag.Equals("enemy4"))
        {
            Debug.Log("boaner4");
        }
    
    }
}
