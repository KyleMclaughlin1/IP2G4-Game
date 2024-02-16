using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    [Tooltip("Transform target for the camera to follow")]
    public Transform target;
    [Tooltip("Space between camera and player")]
    public float distance = 10f;
    void Update()
    {
        transform.position = new Vector3(target.position.x, transform.position.y, (target.position.z-distance)); //Moves camera to target's X and Z position while keeping originaly Y position.
    }
}
