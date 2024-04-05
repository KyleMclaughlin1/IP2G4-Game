using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDelete : MonoBehaviour
{

    //This is a script to delete the bears hitbox objects quickly after spawning them, to not let the player get hit twice

    private void LateUpdate()
    {
        StartCoroutine(deleteObject());
    }

    IEnumerator deleteObject()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

}
