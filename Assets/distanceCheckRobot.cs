using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class distanceCheckRobot : MonoBehaviour
{
    public Transform player;

    public float maxDistance = 5f;

    public float minDistance = 10f;

    public bool playerInRange = false;

    public Enemymovement EM;

    public float timeBetweenShots = 10f;

    public float timer = 0f;

    public GameObject bullet;

    public GameObject firePoint;

    public float distance;

    void Start()
    {
        player = GameObject.Find("Player").transform;

    }

    void Update()
    {
        timer += Time.deltaTime;

        distance = Vector3.Distance(this.transform.position, player.position);

        if (timer >= timeBetweenShots && distance >= minDistance && distance <= maxDistance)
        {
            playerInRange = true;
            StartCoroutine(shootPlayer());

            timer = 0f;
        }
        else
        {
            playerInRange = false;
        }
    }

    public IEnumerator shootPlayer()
    {
        EM.enabled=(false);
        Debug.Log(EM.enabled);
        GameObject newBullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);

        yield return new WaitForSeconds(2);

        EM.enabled = true;
    }
}
