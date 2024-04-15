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

    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        float distance = Vector3.Distance(this.transform.position, player.position);

        if (timer <= 0 && distance >= minDistance && distance <= maxDistance)
        {
            playerInRange = true;
            timer = 0f;
            shootPlayer();

            timer = timeBetweenShots;

        }
        else
        {
            playerInRange = false;
        }
    }

    public IEnumerator shootPlayer()
    {
        EM.enabled=(false);

        GameObject newBullet = Instantiate(bullet, this.transform.position, this.transform.rotation);

        yield return new WaitForSeconds(2);
        EM.enabled = true;
    }
}
