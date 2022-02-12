using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowParentMovement : MonoBehaviour
{
    public static ArrowParentMovement instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
