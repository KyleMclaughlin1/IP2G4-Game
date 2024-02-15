using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TankMovement : MonoBehaviour
{

    [Header("Speed")]
    [SerializeField]
    [Tooltip("How fast the tank is currently moving, not to be edited")]
    private float tankSpeed = 0f;
    [Tooltip("Max speed of the Tank")]
    public float maxSpeed = 60f;
    [Tooltip("Acceleration of the Tank")]
    public float accelRate = 30f;
    [Tooltip("Decceleration when reverse input is pressed")]
    public float deccelRate = 15f;
    [Tooltip("Decceleration when no input is pressed")]
    public float decayRate = 7.5f;
    [Tooltip("How fast the tank turns")]
    public float turnSpeed = 45f;

    [Header("Effects")]
    [Tooltip("Lowest speed for dust particles to appear")]
    public float minDustSpeed;
    [Tooltip("Speed at which dust particles reach max size")]
    public float maxDustSpeed;
    [Tooltip("Rate at which dust particles are spawned")]
    public float dustSpawnRate;

    private float dustTimer;
    //Tracks dust spawn rate

    [Tooltip("Object reference to dust effect")]
    public GameObject dustObject;

    [Header("Input")]
    [Tooltip("What range to consider input from a input axis as no input (some controllers may default to 0.1 instead of 0")] 
    public float axisDeadZone = 0.3f;

    private Rigidbody rb; // Reference to RigidBody

    // Axis names are "Horizontal" and "Vertical"

    void Start()
    {
        rb = GetComponent<Rigidbody>(); //Get reference to Rigidbody
    }

    // Update is called once per frame
    void Update()
    {
        float x_Inpt = Input.GetAxis("Horizontal");
        float y_Inpt = Input.GetAxis("Vertical");
        //Store input in a variable, for code readability  


        if (x_Inpt > axisDeadZone || x_Inpt < -axisDeadZone) //If player is pressing left or right on the x input axis 
        {
            transform.Rotate(transform.up * x_Inpt * turnSpeed * Time.deltaTime);
            //Turn tank
        }

        if (y_Inpt < axisDeadZone && y_Inpt > -axisDeadZone) // If player is not holding a direction on the y input axis
        { 
            tankSpeed -= decayRate * Time.deltaTime;
            //Slow tank down by decay rate, as player is not pushing the tank forward 

        }
        else
        {
            if (y_Inpt > 0) //If player is holding up
            {
                tankSpeed += accelRate * Time.deltaTime * y_Inpt;
                //accelerate tank by increasing tankSpeed
            }
            else //Player is holding down
            {
                tankSpeed += deccelRate * Time.deltaTime * y_Inpt;
                // deccelerate tank by deccreasing tank speed (y_Inpt is negative) 
            }
        }

        tankSpeed = Mathf.Clamp(tankSpeed, 0, maxSpeed);
        //Limit tank from exceeding max speed

        if(tankSpeed > minDustSpeed) //If tank is moving fast enough to spawn dust particles
        {
            dustTimer -= Time.deltaTime;
            //Decrease dust Timer by time
            
            if(dustTimer <= 0f){
                dustTimer = dustSpawnRate;
                //Reset spawn timer
                GameObject dustPar = Instantiate(dustObject);
                //Spawn dust cloud
                dustPar.transform.position = transform.position; 
                //set dust cloud location to tank location
                if(dustPar.GetComponent<ObjectShrink>()){
                    dustPar.GetComponent<ObjectShrink>().startScale = Mathf.InverseLerp(minDustSpeed,maxDustSpeed, tankSpeed);
                    //Sends starting scale value to object shrink script if the dust particles uses that script
                }
            }

        }


        rb.velocity = transform.forward * tankSpeed;
        //Set forward velocity to tankSpeed 

    }
}
