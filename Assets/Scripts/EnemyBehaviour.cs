using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    [Tooltip("How much health the enemy starts with")]
    private int startHealth = 5;
    
    public HealthSystem healthSystem;
    
    void Awake(){
         healthSystem = new HealthSystem(startHealth, startHealth);
    }


    // Update is called once per frame
    void Update()
    {
        if(healthSystem.currentHealth <= 0)
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
            if (other.gameObject.GetComponent<BulletBehaviour>())
            {
                Hit(other.gameObject.GetComponent<BulletBehaviour>().bulletDamage);
            }
            else
            {
                Hit(1);
            }
        }
    }
}
