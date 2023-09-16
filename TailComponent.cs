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

    void Start()
    {
        value = 1;
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
}
