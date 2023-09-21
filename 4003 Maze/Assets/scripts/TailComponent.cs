using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailComponent : MonoBehaviour
{
    [SerializeField]
    private int value;
    [SerializeField]
    private MeshRenderer renderer;
    [SerializeField]
    private Material value1Mat, value2Mat, value3Mat, value4Mat, value5Mat;

    void Start()
    {
        value = 1;
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
            renderer.material = value1Mat;
        }
        else if (value == 2)
        {
            renderer.material = value2Mat;
        }
        else if (value == 3)
        {
            renderer.material = value3Mat;
        }
        else if (value == 4)
        {
            renderer.material = value4Mat;
        }
        else if (value==5)
        {
            renderer.material = value5Mat;
        }
        else
        {
            Debug.LogError("no additional tail components allowed");
        }
    }
}
