using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSort : MonoBehaviour
{
    public float minX;
    public float maxX;
    public LayerMask layerMask;
    public float distance;
    public float moveSpeed;

    public List<GameObject> arrows;
    public GameObject arrowPrefab;
    public Transform arrowParent;

    private void Start()
    {
        GetRay();
    }

    void Update()
    {
        MoveForward();
        if (Input.GetKey(KeyCode.Mouse0))
        {
            GetRay();
        }
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    void MoveObjects(Transform objectTransform, float degree)
    {
        Vector3 position = Vector3.zero;
        position.x = Mathf.Cos(degree * Mathf.Deg2Rad);
        position.y = Mathf.Sin(degree * Mathf.Deg2Rad);
        objectTransform.localPosition = position * (1 / distance);
        objectTransform.localPosition =
            new Vector3(Mathf.Clamp(objectTransform.localPosition.x, -3, 3),
            Mathf.Clamp(objectTransform.localPosition.y, -3, 3), objectTransform.localPosition.z);
    }

    void Sort()
    {
        float angle;
        float arrowCount = arrows.Count;
        angle = 360 / arrowCount;

        for (int i = 0; i < arrowCount; i++)
        {
            MoveObjects(arrows[i].transform, i * angle);
        }
    }

    void SwipeMovement()
    {
        arrowParent.transform.position = Vector3.Lerp(arrowParent.transform.position,
            new Vector3(distance, arrowParent.transform.position.y,
            arrowParent.transform.position.z), Time.deltaTime * 10);
    }

    void GetRay()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.transform.position.z;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, layerMask))
        {
            Vector3 mouse = hit.point;

            mouse.x = Mathf.Clamp(mouse.x, minX, maxX);

            distance = mouse.x;

            Sort();
            SwipeMovement();
        }
    }
}
