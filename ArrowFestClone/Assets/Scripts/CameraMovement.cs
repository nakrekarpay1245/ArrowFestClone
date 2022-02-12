using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float followSpeed;

    [SerializeField]
    private float zOffset;

    [SerializeField]
    private float yOffset;

    public GameObject target;

    public static CameraMovement instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x,
                target.transform.position.y + yOffset, target.transform.position.z + zOffset),
                followSpeed * Time.deltaTime);
        }
    }

    public void FinishGame()
    {
        Invoke("NextLevel", 5);
        yOffset = 0;
    }
    void NextLevel()
    {
        SceneManager.LoadScene(0);
    }
}
