using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinihLevel : MonoBehaviour
{
    bool isCollide;

    public GameObject lastObstacle;
    public GameObject lastRocket;

    public GameObject arrowCountText;

    public GameObject nextButton;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            if (!isCollide)
            {
                isCollide = true;
                gameObject.GetComponent<BoxCollider>().enabled = false;

                lastObstacle.transform.position =
                    new Vector3(lastObstacle.transform.position.x, lastObstacle.transform.position.y,
                    transform.position.z + 25 + ArrowSort.instance.arrows.Count * 4);

                arrowCountText.SetActive(false);
                lastRocket.SetActive(true);
                lastObstacle.SetActive(true);
                ArrowSort.instance.arrowParent.gameObject.SetActive(false);
                CameraMovement.instance.target = lastRocket;
                lastRocket.GetComponent<ArrowMovement>().targetObstacle = gameObject;
                Invoke("XCalculate", 2);
            }
        }
    }

    private void XCalculate()
    {
        nextButton.SetActive(true);
        lastRocket.GetComponent<ArrowMovement>().targetObstacle = lastObstacle;
    }
}
