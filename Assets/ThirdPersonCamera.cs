using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; 
    public float distance = 5.0f;
    public float yOffset = 1.0f; 
    public float xSens = 5.0f; 
    public float ySens = 1.0f;
    public LayerMask collisionMask;

    private Vector3 currentRotation;
    private float yaw;
    private float pitch;

    private void Start()
    {

        currentRotation = transform.eulerAngles;
        yaw = currentRotation.y;
        pitch = currentRotation.x;
        Cursor.lockState = CursorLockMode.Locked; 
    }

    private void LateUpdate()
    {

        yaw += Input.GetAxis("Mouse X") * xSens;
        pitch -= Input.GetAxis("Mouse Y") * ySens;
        pitch = Mathf.Clamp(pitch, -5, 85); 


        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 negDistance = new Vector3(0.0f, yOffset, -distance);
        Vector3 position = rotation * negDistance + target.position;


        RaycastHit hit;
        if (Physics.Raycast(target.position, (position - target.position).normalized, out hit, distance, collisionMask))
        {
            position = hit.point - (target.position - position).normalized * 0.5f; 
        }

        transform.rotation = rotation;
        transform.position = position;

        if (Input.GetKeyDown(KeyCode.R))
        {
            yaw = target.eulerAngles.y;
            pitch = target.eulerAngles.x;
        }
    }
}