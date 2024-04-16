using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehaviour : MonoBehaviour
{
    public HealthSystem healthSystem;

    private float timer;

    public Rigidbody body;

    public float killTime;
    public float bulletSpeed;

    void Update()
    {
        body.AddForce(transform.forward * bulletSpeed, ForceMode.Force);

        timer += Time.deltaTime;

        if (timer >= killTime)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
