using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpDrop : MonoBehaviour
{
    public bool firstPickup = false;

    void OnTriggerEnter(Collider c)
    {
        PlayerBehaviour playerBehaviour = c.GetComponent<PlayerBehaviour>();

        if (playerBehaviour != null)
        {
            Debug.Log("picked up health");
            gameObject.SetActive(false);
            playerBehaviour.healthIncrease();
            if (firstPickup == false)
            {
                firstPickup = true;
                playerBehaviour.showHpTut();
            }
        }
    }
}
