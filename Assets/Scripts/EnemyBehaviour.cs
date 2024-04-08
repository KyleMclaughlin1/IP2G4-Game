using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    [Tooltip("How much health the enemy starts with")]
    private int startHealth = 5;
    
    public HealthSystem healthSystem;
    public GameObject dmgBuffDrop;
    public GameObject healthUpDrop;
    public float randomNum;
    public GameObject dropPosition;
    public GameObject deathAnim;
    
    void Awake(){
         healthSystem = new HealthSystem(startHealth, startHealth);
    }


    // Update is called once per frame
    void Update()
    {

        if(healthSystem.currentHealth <= 0)
        {
            GameObject deadBear = Instantiate(deathAnim, this.transform.position, this.transform.rotation);
            Destroy(gameObject);
            dropBuff();
        }
    }

    private void Hit(int Damage)
    {
        // use "DamageUnit" from Health System to damage player
        healthSystem.DamageUnit(Damage);
    }
    private void Hit(float Damage)
    {
        // use "DamageUnit" from Health System to damage player
        healthSystem.DamageUnit((int)Damage);
    }




    void OnCollisionEnter(Collision other)
    {
        // take damage if the player gets hit by an enemy
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

    public void dropBuff()
    {
        randomNum = Random.Range(1, 4);
        Vector3 position = dropPosition.transform.position;
        if (randomNum == 2)
        {
            GameObject dmgUp = Instantiate(dmgBuffDrop, position, Quaternion.identity);
        }
        if (randomNum == 3)
        {
            GameObject hpUp = Instantiate(healthUpDrop, position, Quaternion.identity);
        }
    }
}
