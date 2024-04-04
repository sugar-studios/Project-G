using UnityEngine;

namespace ProjectG.Player
{
    public class CameraControls : MonoBehaviour
    {
        public GameObject cameraRig;
        public float rotationSpeed = 5.0f;

        private Quaternion defaultRotation; // To store the default rotation of the camera

        void Start()
        {
            // Store the initial rotation of the camera as the default rotation
            defaultRotation = cameraRig.transform.rotation;
        }

        void Update()
        {
            float horizontalRotation = Input.GetAxis("Mouse X") * rotationSpeed;
            float verticalRotation = Input.GetAxis("Mouse Y") * rotationSpeed / 2;

            // Apply rotations based on mouse movement
            RotateX(horizontalRotation);
            RotateY(verticalRotation);

            // Reset the camera's rotation when the "R" key is pressed
            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetCameraRotation();
            }
        }

        private void RotateY(float val)
        {
            cameraRig.transform.Rotate(-val, 0, 0, Space.Self);
        }

        private void RotateX(float val)
        {
            cameraRig.transform.Rotate(0, val, 0, Space.World);
        }

        private void ResetCameraRotation()
        {
            // Reset the camera's rotation to the default state
            cameraRig.transform.rotation = defaultRotation;
        }
    }
}
