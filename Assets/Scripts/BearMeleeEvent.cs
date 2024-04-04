using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearMeleeEvent : MonoBehaviour
{
    public GameObject bearHitbox;
    private Vector3 storedposition;
    public void BearAttackLands() // Event created inside the bear attack animation, can be found in the animations folder in assets
    {
       // storedposition = bearHitbox.transform.position;
       // bearHitbox.SetActive(true);
        GameObject newHitBox = Instantiate(bearHitbox);
        newHitBox.SetActive(true);
        newHitBox.transform.position = bearHitbox.transform.position;
        newHitBox.GetComponent<InstantDelete>().enabled = true;
       // bearHitbox.transform.Translate(Vector3.up * 20);
    }
}
