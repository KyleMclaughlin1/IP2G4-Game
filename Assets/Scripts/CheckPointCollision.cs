using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointCollision : MonoBehaviour
{

    private LapChecker lapCheck;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            lapCheck.PlayerHitCheckpoint(this);
        }
    }

    public void SetCheckPoint(LapChecker lapCheckerScript)
    {
        lapCheck = lapCheckerScript;
        //Triggered by the lapchecker script on awake, sets lapCheck variable to the lapchecker script to get a reference
    }

}
