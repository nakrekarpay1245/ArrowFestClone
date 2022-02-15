using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    bool isCollide;

    public int count;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            if (!isCollide)
            {
                isCollide = true;
                gameObject.GetComponent<Collider>().enabled = false;
                ArrowSort.instance.targetObstacle = this.gameObject;
                ArrowSort.instance.Shoot(count);
            }
        }
    }

}
