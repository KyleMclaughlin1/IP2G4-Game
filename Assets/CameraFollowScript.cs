using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    [Tooltip("Transform target for the camera to follow")]
    public Transform target;
    void Update()
    {
        transform.position = new Vector3(target.position.x, transform.position.y, target.position.z); //Moves camera to target's X and Z position while keeping originaly Y position.
    }
}
