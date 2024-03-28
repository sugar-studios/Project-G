using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform cameraTransform; // Assign your main camera's transform here in the inspector

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
    private float rotationSpeed = 5.0f; // Adjusted for camera rotation
    public float cameraSensitivity = 100f; // Sensitivity of camera rotation

    private float cameraPitch = 0f; // Vertical rotation of the camera

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            controller = gameObject.AddComponent<CharacterController>();
        }
    }

    void Update()
    {
        MovePlayer();
        RotateCamera();
        HandleJump();
    }

    void MovePlayer()
    {
        groundedPlayer = controller.isGrounded;

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 move = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0) * input; // Make movement relative to camera's orientation
        controller.Move(move * Time.deltaTime * playerSpeed);

        ApplyGravity();
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;

        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -40, 75);

        cameraTransform.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * mouseX);
    }

    void ApplyGravity()
    {
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void HandleJump()
    {
        if (Input.GetKey(KeyCode.Space) && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
    }
}
