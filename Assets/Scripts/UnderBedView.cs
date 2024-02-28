using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderBedView : MonoBehaviour
{

    public GameObject[] bedFrames;

    void OnTriggerEnter(Collider c)
    {
        PlayerBehaviour playerBehaviour = c.GetComponent<PlayerBehaviour>();

        if(playerBehaviour != null)
        {
            for (int i = 0; i < bedFrames.Length; i++)
            {
                bedFrames[i].SetActive(false);
                Debug.Log("is under bed");
            }
        }
    }

    void OnTriggerExit(Collider c2)
    {
        PlayerBehaviour playerBehaviour = c2.GetComponent<PlayerBehaviour>();

        if (playerBehaviour != null)
        {
            for (int i = 0; i < bedFrames.Length; i++)
            {
                bedFrames[i].SetActive(true);
                Debug.Log("isnt under bed");
            }
        }
    }


}
