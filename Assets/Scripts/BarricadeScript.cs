using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeScript : MonoBehaviour
{
public GameObject barricade;
public bool setActiv = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            barricade.SetActive(setActiv);
        }
    }
}
