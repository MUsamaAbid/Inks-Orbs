using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMarker : MonoBehaviour
{
    public float rotationSpeed = 10f;

    void Update()
    {
        // Get the current rotation angles
        float currentRotationX = transform.rotation.eulerAngles.x;
        float currentRotationY = transform.rotation.eulerAngles.y;

        // Set the desired rotation angles
        float desiredRotationX = -90f;
        float desiredRotationZ = transform.rotation.eulerAngles.z + rotationSpeed * Time.unscaledDeltaTime;

        // Create the desired rotation Quaternion
        Quaternion desiredRotation = Quaternion.Euler(desiredRotationX, currentRotationY, desiredRotationZ);

        // Apply the rotation to the object
        transform.rotation = desiredRotation;
    }
}
