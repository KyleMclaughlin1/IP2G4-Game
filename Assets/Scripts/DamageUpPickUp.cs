using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUpPickUp : MonoBehaviour
{

    public bool isBuffed = false;
    public bool firstPickUp = false;
    public GameObject tutorialText;

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
                DmgTutorial();
            }
        }
    }

    public void DmgTutorial()
    {
        firstPickUp=true;

    }
}
