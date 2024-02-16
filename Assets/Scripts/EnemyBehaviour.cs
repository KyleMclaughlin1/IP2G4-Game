using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public HealthSystem healthSystem = new HealthSystem(5, 5);
    
    // Update is called once per frame
    void Update()
    {
        if(healthSystem.currentHealth == 0)
        {
            Destroy(gameObject);
        }
    }

    private void Hit(int Damage)
    {
        // use "DamageUnit" from Health System to damage player
        healthSystem.DamageUnit(Damage);
    }

    void OnCollisionEnter(Collision other)
    {
        // take damage if the player gats hit by an enemy
        if (other.gameObject.CompareTag("bullet"))
        {
            Hit(1);
        }
    }
}
