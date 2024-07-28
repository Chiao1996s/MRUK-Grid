using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Transform cameraRig;

    void Update()
    {
        if (cameraRig != null)
        {
            // Calculate the direction to the camera rig
            Vector3 directionToCamera = cameraRig.position - transform.position;
            directionToCamera.y = 0;  // Ignore the vertical component

            // Calculate the rotation to face the camera rig horizontally
            Quaternion rotationToCamera = Quaternion.LookRotation(directionToCamera);

            // Apply the rotation to the object
            transform.rotation = rotationToCamera;
        }
    }
}