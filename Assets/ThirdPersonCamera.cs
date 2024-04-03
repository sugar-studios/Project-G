using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // The object to follow
    public float distance = 5.0f; // Distance from the target object
    public float yOffset = 1.0f; // Vertical offset from the target object
    public LayerMask collisionMask; // Collision mask to prevent clipping through objects

    private Vector3 currentRotation;
    private float yaw;
    private float pitch;

    private void Start()
    {
        // Initialize the rotation of the camera
        currentRotation = transform.eulerAngles;
        yaw = currentRotation.y;
        pitch = currentRotation.x;
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
    }

    private void LateUpdate()
    {
        // Handle the orbiting effect around the target
        yaw += Input.GetAxis("Mouse X");
        pitch -= Input.GetAxis("Mouse Y");
        pitch = Mathf.Clamp(pitch, -5, 85); // Clamp the pitch to prevent flipping

        // Calculate the new rotation and apply it to the camera
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 negDistance = new Vector3(0.0f, yOffset, -distance);
        Vector3 position = rotation * negDistance + target.position;

        // Adjust the camera position using raycasting to prevent clipping through objects
        RaycastHit hit;
        if (Physics.Raycast(target.position, (position - target.position).normalized, out hit, distance, collisionMask))
        {
            position = hit.point - (target.position - position).normalized * 0.5f; // Offset slightly to prevent clipping into the collider
        }

        // Set the calculated position and rotation
        transform.rotation = rotation;
        transform.position = position;

        // Reset camera distance and position
        if (Input.GetKeyDown(KeyCode.R))
        {
            yaw = target.eulerAngles.y;
            pitch = target.eulerAngles.x;
        }
    }
}
