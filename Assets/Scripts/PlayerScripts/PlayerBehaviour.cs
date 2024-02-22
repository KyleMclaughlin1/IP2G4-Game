using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    public CannonControl bullet;

    void Start()
    {

    }

    void Update()
    {      
        //replace once enemies introduced
        if (Input.GetKeyDown(KeyCode.G))
        {
            PlayerHit(2);
        }
        //replace once pickups introduced
        if (Input.GetKeyDown(KeyCode.H))
        {
            PlayerHeal(1);
        }

        //if the player drops to 0 health, kill them
        if (GameManager.gameManager.playerHealth.Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void PlayerHit(int Damage)
    {
        // use "DamageUnit" from Health System to damage player
        GameManager.gameManager.playerHealth.DamageUnit(Damage);
        Debug.Log(GameManager.gameManager.playerHealth.Health);
    }

    private void PlayerHeal(int Healing)
    {
        // use "HealUnit" from Health System to heal player
        GameManager.gameManager.playerHealth.HealUnit(Healing);
        Debug.Log(GameManager.gameManager.playerHealth.Health);
    }

    void OnCollisionEnter(Collision other)
    {
        // take damage if the player gats hit by an enemy
        if (other.gameObject.CompareTag("enemy"))
        {
            PlayerHit(2);
            
        }
    }

    public void increaseDamage()
    {
        bullet.bulletDamageMultiplier = bullet.bulletDamageMultiplier + 1;
        StartCoroutine(buffTimer());
    }

    IEnumerator buffTimer()
    {
        yield return new WaitForSeconds(15);
        bullet.bulletDamageMultiplier = bullet.bulletDamageMultiplier - 1;
    }

    public void healthIncrease()
    {
        PlayerHeal(1);
    }
}
