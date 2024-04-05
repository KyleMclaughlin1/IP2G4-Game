using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bearDeath : MonoBehaviour
{
    // Start is called before the first frame update

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 2)
        {
            Destroy(gameObject);
        }
    }

}
