using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonControl : MonoBehaviour
{
    private Vector3 mousePos; //Tracks mouse position

    [Header("Setup")]
    [Tooltip("Whether to use gamepad controls or mouse controls")]
    public bool usingGamePad;
    [Tooltip("Reference to firepoint object for location to fire bullets from")]
    public Transform firePoint;
    [Tooltip("Reference to bullet prefab to fire")]
    public GameObject bullet;

    [Header("CannonStats")]
    [Tooltip("Whether cannon fires automatically or on button press")]
    public bool cannonAuto = false;
    [Tooltip("How much damage the bullet does (bullet damage is multiplied by this value")]
    public int bulletDamageMultiplier = 1;
    [Tooltip("How long it takes for the cannon to fire again after firing")]
    public float fireRate = 1f;

    private float fireTimer = 0f; //Timer for fire rate;
    


    void Update()
    {
        //Most of this script is taken from here: https://forum.unity.com/threads/2d-character-rotation-towards-mouse.457126/



        if (usingGamePad)
        {
            float angle = Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            angle -= 90f;

            transform.rotation = Quaternion.Euler(new Vector3(0, -angle, 0));
        }
        else
        {
            mousePos = Input.mousePosition; //Get mouse position from input

            Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
            //Get cannon object position on screen through the camera
            mousePos.x = mousePos.x - objectPos.x;
            mousePos.y = mousePos.y - objectPos.y;
            //Get the difference between the mouse position and cannon position

            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            //Get the angle to the mouse position using maths I don't fully understand
            angle -= 90f;
            //The cannon defaults facing up, so we need to turn the angle to accomodate
            transform.rotation = Quaternion.Euler(new Vector3(0, -angle, 0));
            //Sets the cannon pivot's rotation to the angle we made, turning the cannon through its parent

            if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse0) && cannonAuto) && (fireTimer <= 0f))
            {
                //Fire bullet on mouse click, or mouse hold if auto firing cannon is turned on 

                GameObject newBullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
                newBullet.transform.Rotate(Vector3.forward * 90);
                //face the bullet sideways instead of upwards
                newBullet.transform.Rotate(Vector3.right * 90);
                //fix the angle to work with the cannon defaulting upwards again
                if (newBullet.GetComponent<BulletBehaviour>())
                {
                    newBullet.GetComponent<BulletBehaviour>().bulletDamage *= bulletDamageMultiplier;
                    //If the chosen bullet has the bullet behaviour script, multiply its damage by the tanks
                }



                fireTimer = fireRate;
                //Set firetimer to firerate so the player cannot fire again until the firerate time passes

            }

            fireTimer -= Time.deltaTime;
            //Decrease firetimer by time to allow for shot reload
        }
    }
}
