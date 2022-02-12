using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinihLevel : MonoBehaviour
{
    bool isCollide;

    public GameObject bigRocket;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            if (!isCollide)
            {
                isCollide = true;
                gameObject.GetComponent<BoxCollider>().enabled = false;
                other.gameObject.transform.parent.gameObject.SetActive(false);
                CameraMovement.instance.target = bigRocket;
                bigRocket.SetActive(true);
            }
        }
    }
}
