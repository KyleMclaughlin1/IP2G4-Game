using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearBehaviour : EnemyBehaviour
{
    public Animator bearAnim;
    public Enemymovement bearMovement;

    public float bearWalkSpeed = 15f;
    public float bearChaseSpeed = 34f;
    public float chaseWarmUp = 3f;
    private float chaseTimer = 0f;


    //e
    private void Start()
    {
        bearMovement.agent.speed = bearWalkSpeed;
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
        }else if(other.gameObject.CompareTag("Player"))
        {
            bearAnim.SetTrigger("BearAttack");
        }
    }
    //Unity doesn't like monobehaviour functions being overriden, so this function is copy pasted




    private void Hit(int Damage)
    {
        // use "DamageUnit" from Health System to damage player
        healthSystem.DamageUnit(Damage);
        bearAnim.SetTrigger("BeenHit");
    }


        private void Hit(float Damage)
    {
        // use "DamageUnit" from Health System to damage player
        healthSystem.DamageUnit((int)Damage);
        bearAnim.SetTrigger("BeenHit");
    }
    //collision enter function has a reference to this function, so unity forces me to copy paste this in unedited

    //More copy pasting, inheritence was probably a bad idea at this point but I don't want to turn back
    void Update()
    {
        bearAnim.SetBool("BearPatrol", false); // Bool will be set to true if player isn't in range, this presets it to true if they are



        if (healthSystem.currentHealth <= 0)
        {
            GameObject deadBear = Instantiate(deathAnim, this.transform.position, this.transform.rotation);
            Destroy(gameObject);
            dropBuff();
        }

        if (bearMovement.playerInRange && chaseTimer < chaseWarmUp)
        {
            chaseTimer += Time.deltaTime;
            if(chaseTimer >= chaseWarmUp)
            {
                bearMovement.agent.speed = bearChaseSpeed;
                bearAnim.SetBool("BearChase", true);
            }
        }
        else
        {
            bearAnim.SetBool("BearPatrol", true);
        }


    }


}
