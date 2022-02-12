using UnityEngine;

public class BigRocket : MonoBehaviour
{
    public float speed;

    public ParticleSystem explosionParticle;
    private void Start()
    {
        Invoke("Explosion", 10);
    }
    void Update()
    {
        transform.Translate(speed * Vector3.up * Time.deltaTime);
        speed += Time.deltaTime * 5;
    }

    void Explosion()
    {
        Instantiate(explosionParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
        CameraMovement.instance.FinishGame();
    }
}
