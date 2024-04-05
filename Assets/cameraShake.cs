using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera CinemachineVirtualCamera;
    public float ShakeIntensity = 1f;

    private CinemachineBasicMultiChannelPerlin _cbmcp;

    public GameObject MainCam;
    public PlayerBehaviour PB;

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

    }

    void StopShake()
    {
        CinemachineBasicMultiChannelPerlin _cbmcp = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = 0f;

        transform.position = MainCam.transform.position;
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
        if (PB.playerHit == true)
        {
            ShakeCamera();
        }
        if (!PB.playerHit)
        {
            StopShake();
        }
    }
}
