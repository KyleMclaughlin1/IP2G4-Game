using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    public CannonControl bullet;
    public GameObject buffLight;
    public AudioSource BatteryAudioSource;
    public GameObject DmgTut;
    public GameObject hpTut;

    void Start()
    {
        buffLight.SetActive(false);
        DmgTut.SetActive(false);
        hpTut.SetActive(false);
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
            GameManager.gameManager.gameOverded();
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
        buffLight.SetActive(true);
        BatteryAudioSource.Play();
        bullet.bulletDamageMultiplier = bullet.bulletDamageMultiplier + 1;
        StartCoroutine(buffTimer());
    }

    IEnumerator buffTimer()
    {
        yield return new WaitForSeconds(10);
        bullet.bulletDamageMultiplier = bullet.bulletDamageMultiplier - 1;
        buffLight.SetActive(false);
    }

    public void healthIncrease()
    {
        PlayerHeal(1);
        BatteryAudioSource.Play();
    }

    public void showDmgTut()
    {
        DmgTut.gameObject.SetActive(true);
        StartCoroutine(dmgTutWait());
    }

    IEnumerator dmgTutWait()
    {
        yield return new WaitForSeconds(15);
        DmgTut.gameObject.SetActive(false);
    }

    public void showHpTut()
    {
        hpTut.gameObject.SetActive(true);
        StartCoroutine(hpTutWait());
    }

    IEnumerator hpTutWait()
    {
        yield return new WaitForSeconds(15);
        hpTut.gameObject.SetActive(false);
    }
}
