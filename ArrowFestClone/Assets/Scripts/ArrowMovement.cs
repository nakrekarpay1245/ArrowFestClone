using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;

    public GameObject targetObstacle;
    private void Update()
    {
        if (targetObstacle != null)
        {
            LookToTarget();
            MoveToTarget();
        }
    }

    private void MoveToTarget()
    {
        transform.position = Vector3.Lerp(transform.position,
                      targetObstacle.transform.position, Time.deltaTime * moveSpeed);
    }

    private void LookToTarget()
    {
        Debug.Log("Look To Target");
        Quaternion rotationTarget = Quaternion.LookRotation(targetObstacle.transform.position -
            transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation,
            rotationTarget, Time.deltaTime * rotateSpeed);
    }

}
