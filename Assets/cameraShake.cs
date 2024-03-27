using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera CinemachineVirtualCamera;
    private float ShakeIntensity = 1f;
    private float ShakeTime = 0.2f;

    private float Timer;
    private CinemachineBasicMultiChannelPerlin _cbmcp;

    public GameObject MainCam;

    void Awake()
    {
        CinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        StopShake();
    }

    public void ShakeCamera()
    {

        CinemachineBasicMultiChannelPerlin _cbmcp = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = ShakeIntensity;

        Timer = ShakeTime;
    }

    void StopShake()
    {
        CinemachineBasicMultiChannelPerlin _cbmcp = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = 0f;
        Timer = 0f;

        transform.position = MainCam.transform.position ;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShakeCamera();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StopShake();
        }


            if (Timer <= 0)
        {

            Timer += Time.deltaTime;

            if (Timer >= 2f)
            {
                StopShake();
            }
        }
    }
}
