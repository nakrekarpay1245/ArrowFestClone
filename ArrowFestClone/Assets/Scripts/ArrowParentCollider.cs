using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowParentCollider : MonoBehaviour
{
    public static ArrowParentCollider instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void AdArrow(int count)
    {
        ArrowSort.instance.AdArrow(count);
    }
    public void RemoveArrow(int count)
    {
        ArrowSort.instance.RemoveArrow(count);
    }
    public void DivideArrow(int count)
    {
        ArrowSort.instance.DivideArrow(count);
    }
    public void MultiplyArrow(int count)
    {
        ArrowSort.instance.MultiplyArrow(count);
    }
}
