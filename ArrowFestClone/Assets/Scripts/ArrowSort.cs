using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ArrowSort : MonoBehaviour
{
    public float minX;
    public float maxX;
    public LayerMask layerMask;
    public float distance;
    private float moveSpeed;
    public float speed;

    public List<GameObject> arrows;
    public GameObject arrowPrefab;
    public Transform arrowParent;

    public GameObject tutorial;
    public GameObject targetObstacle;

    bool isGameStart;
    bool isShoot;

    public Text arrowCountText;

    public static ArrowSort instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        GetRay();
    }

    void Update()
    {
        arrowCountText.text = arrows.Count.ToString();
        MoveForward();
        if (Input.GetKey(KeyCode.Mouse0))
        {
            GetRay();
            if (!isGameStart)
            {
                isGameStart = true;
                tutorial.SetActive(false);
                moveSpeed = speed;
            }
        }
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    void MoveObjects(Transform objectTransform, float degree)
    {
        Vector3 position = Vector3.zero;
        float oneDivideDistance = 1 / distance;
        oneDivideDistance = Mathf.Clamp(oneDivideDistance, -3, 3);
        position.x = Mathf.Cos(degree * Mathf.Deg2Rad);
        position.y = Mathf.Sin(degree * Mathf.Deg2Rad);
        objectTransform.localPosition = position * oneDivideDistance;
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

        arrowParent.transform.Rotate(Vector3.forward * Time.deltaTime * 500 * distance);
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

            if (!isShoot)
            {
                Sort();
            }
            SwipeMovement();
        }
    }

    public void Shoot(int count)
    {
        StartCoroutine(ShootCoroutine(count));
    }

    public IEnumerator ShootCoroutine(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(.1f);
            GameObject currentArrow = arrows[0];
            arrows.Remove(currentArrow);
            currentArrow.transform.parent = null;
            currentArrow.GetComponent<ArrowMovement>().targetObstacle = this.targetObstacle;
            Destroy(currentArrow, 1);
            //Debug.Log("Nabre : " + currentArrow.name);
        }
    }

    public void AdArrow(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject currentArrow = Instantiate(arrowPrefab, arrowParent);
            currentArrow.transform.rotation = arrows[i].transform.rotation;
            arrows.Add(currentArrow);
            Sort();
        }
    }
    public void RemoveArrow(int count)
    {
        if (count < arrows.Count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject currentArrow = arrows[arrows.Count - 1];
                currentArrow.SetActive(false);
                arrows.Remove(currentArrow);
                currentArrow.transform.parent = null;
                Destroy(currentArrow, 1);
                Sort();
            }
        }
        else
        {
            Debug.LogWarning("Game Over");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public void DivideArrow(float count)
    {
        float divideCount = Mathf.Ceil(arrows.Count / count);
        float arrowCount = arrows.Count - divideCount;
        if (arrowCount < arrows.Count)
        {
            for (int i = 0; i < arrowCount; i++)
            {
                GameObject currentArrow = arrows[arrows.Count - 1];
                currentArrow.SetActive(false);
                arrows.Remove(currentArrow);
                currentArrow.transform.parent = null;
                Destroy(currentArrow, 1);
                Sort();
            }
        }
        else
        {
            Debug.LogWarning("Game Over");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public void MultiplyArrow(int count)
    {
        int multiplyCount = arrows.Count * (count - 1);
        for (int i = 0; i < multiplyCount; i++)
        {
            GameObject currentArrow = Instantiate(arrowPrefab, arrowParent);
            currentArrow.transform.rotation = arrows[i].transform.rotation;
            arrows.Add(currentArrow);
            Sort();
        }
    }

    public void SceneLoader(int value)
    {
        SceneManager.LoadScene(value);
    }
}
