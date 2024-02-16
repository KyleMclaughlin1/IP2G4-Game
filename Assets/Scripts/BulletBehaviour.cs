using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public Rigidbody body;
    public float bulletSpeed;

    // Update is called once per frame
    void Update()
    {
        body.AddForce(transform.right * bulletSpeed, ForceMode.Force);
    }
}
