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
    public float maxSpeed = 1000f;
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
    public GameObject slowDustObject;

    [Header("Input")]
    [Tooltip("What range to consider input from a input axis as no input (some controllers may default to 0.1 instead of 0")] 
    public float axisDeadZone = 0.3f;

    private Rigidbody rb; // Reference to RigidBody

    public AudioSource EngineAudioSource;
    public float minPitch;
    public float maxPitch;
    public float tankPitch;
    public float currentTankSpeed;
    public float minAccelSpeed = 1.5f;
    public float maxAccelSpeed = 30f;
    RaycastHit hit;

    // Axis names are "Horizontal" and "Vertical"

    void Start()
    {
        rb = GetComponent<Rigidbody>(); //Get reference to Rigidbody
        EngineAudioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        float x_Inpt = Input.GetAxis("Horizontal");
        float y_Inpt = Input.GetAxis("Vertical");
        //Store input in a variable, for code readability  

        drivingAudio();

        if (currentTankSpeed >= 20)
        {
            dustObject.SetActive(true);
            slowDustObject.SetActive(false);
        }
        else if (currentTankSpeed >= 5)
        {
            slowDustObject.SetActive(true);
            dustObject.SetActive(false);
        }
        else
        {
            slowDustObject.SetActive(false);
            dustObject.SetActive(false);
        }


        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            if (hit.transform.gameObject.tag == "road")
            {
                //Debug.Log("On road");
                accelRate = 15f;
                maxSpeed = 33f;
            }
            else
            {
                //Debug.Log("On floor");
                accelRate = 10f;
                maxSpeed = 15f;
            }
        }

            if (x_Inpt > axisDeadZone || x_Inpt < -axisDeadZone) //If player is pressing left or right on the x input axis 
        {
            transform.Rotate(transform.up * x_Inpt * turnSpeed * Time.deltaTime);
            //Turn tank
        }

        if (y_Inpt < axisDeadZone && y_Inpt > -axisDeadZone && tankSpeed > 0) // If player is not holding a direction on the y input axis
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

        if (y_Inpt < axisDeadZone && y_Inpt > -axisDeadZone && tankSpeed < 0)
        {

            tankSpeed += decayRate * Time.deltaTime;
        }

        tankSpeed = Mathf.Clamp(tankSpeed, -15, maxSpeed);
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

                if(dustPar.GetComponent<ObjectShrink>()){
                    dustPar.GetComponent<ObjectShrink>().startScale = Mathf.InverseLerp(minDustSpeed,maxDustSpeed, tankSpeed);
                    //Sends starting scale value to object shrink script if the dust particles uses that script
                }

                dustPar.transform.position = transform.position; 
                //set dust cloud location to tank location
            }

        }


        rb.velocity = transform.forward * tankSpeed;
        //Set forward velocity to tankSpeed 

    }

    void drivingAudio()
    {
        currentTankSpeed = rb.velocity.magnitude;
        tankPitch = rb.velocity.magnitude / 60f;

        if (currentTankSpeed < minAccelSpeed)
        {
            EngineAudioSource.pitch = minPitch;
        }

        if (currentTankSpeed > minAccelSpeed && currentTankSpeed < maxAccelSpeed)
        {
            EngineAudioSource.pitch = maxPitch + tankPitch;
        }

        if (currentTankSpeed > maxAccelSpeed)
        {
            EngineAudioSource.pitch = maxPitch;
        }
    }

}
