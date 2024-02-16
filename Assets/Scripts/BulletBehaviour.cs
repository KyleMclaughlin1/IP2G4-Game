using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public Rigidbody body;
    [Tooltip("Speed of the bullet")]
    public float bulletSpeed;
    [Tooltip("How long it takes for the bullet to be despawned")]
    public float bulletLife;

    // Update is called once per frame
    void Update()
    {
        body.AddForce(transform.up * bulletSpeed, ForceMode.Force);

        //Destroy bullet after being alive for a set time
        Destroy(gameObject, bulletLife);
    }

    // Destroy bullet on impact
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {

        }
        Destroy(gameObject);
    }
}
