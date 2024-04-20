using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectG.Title
{
    public class TitleCameraRotator : MonoBehaviour
    {
        [SerializeField]
        float rotationSpeed;
        void Update()
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}
