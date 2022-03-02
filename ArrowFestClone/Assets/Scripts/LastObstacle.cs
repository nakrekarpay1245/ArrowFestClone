using UnityEngine;

public class LastObstacle : MonoBehaviour
{
    bool isCollide;

    public ParticleSystem explosionParticle;

    private void Awake()
    {
        explosionParticle = GetComponentInChildren<ParticleSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            if (!isCollide)
            {
                isCollide = true;
                gameObject.GetComponent<Collider>().enabled = false;
                explosionParticle.Play();
                other.gameObject.SetActive(false);

                GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<Rigidbody>().
                    AddForce(Vector3.up * Random.Range(50, 100) +
                    Vector3.forward * Random.Range(50, 100));

                GetComponent<Rigidbody>().
                     AddTorque(Vector3.right * Random.Range(50, 100));
            }
        }
    }

}
