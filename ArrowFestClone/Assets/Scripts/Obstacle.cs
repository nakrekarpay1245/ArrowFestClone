using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    bool isCollide;

    public int count;

    public float speed = 5;

    public ParticleSystem explosionParticle;
    private void Awake()
    {
        explosionParticle = GetComponentInChildren<ParticleSystem>();
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
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
                Invoke("Explosion", 1);
            }
        }
    }

    private void Explosion()
    {
        speed = 0;
        explosionParticle.Play();
        Destroy(gameObject, 1.5f);
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().
            AddForce(Vector3.up * Random.Range(50, 100) +
            Vector3.forward * Random.Range(50, 100) + 
            Vector3.right * transform.position.x * Random.Range(10,25));

        GetComponent<Rigidbody>().
             AddTorque(Vector3.right * Random.Range(50, 100));
    }

}
