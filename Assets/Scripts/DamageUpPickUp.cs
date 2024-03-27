using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUpPickUp : MonoBehaviour
{
    public bool isBuffed = false;
    public bool firstPickUp = false;


    void Start()
    {
        isBuffed = false;
        firstPickUp = false;
    }

    void OnTriggerEnter(Collider c)
    {
        PlayerBehaviour playerBehaviour = c.GetComponent<PlayerBehaviour>();

        if (playerBehaviour != null)
        {
            Debug.Log("picked up");
            isBuffed = true;
            gameObject.SetActive(false);
            playerBehaviour.increaseDamage();
            if (firstPickUp == false)
            {
                firstPickUp = true;
                playerBehaviour.showDmgTut();
            }
        }
    }
}
