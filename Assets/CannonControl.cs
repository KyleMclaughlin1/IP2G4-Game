using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonControl : MonoBehaviour
{
    private Vector3 mousePos; //Tracks mouse position


    [Tooltip("Whether to use gamepad controls or mouse controls")]
    public bool usingGamePad;
    [Tooltip("Reference to firepoint object for location to fire bullets from")]
    public Transform firePoint;
    [Tooltip("Reference to bullet prefab to fire")]
    public GameObject bullet;


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

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //Fire bullet on mouse click

                GameObject newBullet = Instantiate(bullet, firePoint.position, transform.rotation);
                newBullet.transform.Rotate(Vector3.forward * 90);
                //face the bullet sideways instead of upwards
                newBullet.transform.Rotate(Vector3.right * 90);
                //fix the angle to work with the cannon defaulting upwards again

            }

        }
    }
}
