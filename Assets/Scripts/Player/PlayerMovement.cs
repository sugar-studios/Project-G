using UnityEngine;

namespace ProjectG.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public GameObject playerGraphics;
        public Transform camera;

        public float speed = 5f;
        public float turnSmoothTime = 0.1f;
        public float jumpHeight = 2f;
        public float gravityValue = -9.81f;
        public LayerMask groundMask;
        public float groundDistance = 0.4f;
        public Transform groundCheck;

        private CharacterController controller;
        private float turnSmoothVelocity;
        private Vector3 playerVelocity;
        private bool isGrounded;
        private bool shouldJump;

        private void Start()
        {
            controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask) || controller.isGrounded;

            if (isGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = -2f;
            }

            Move();

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                shouldJump = true;
            }
        }

        private void FixedUpdate()
        {
            if (shouldJump)
            {
                playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
                shouldJump = false;
            }

            ApplyGravity();
        }

        void Move()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(playerGraphics.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                playerGraphics.transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
        }

        void ApplyGravity()
        {
            playerVelocity.y += gravityValue * Time.fixedDeltaTime;
            controller.Move(playerVelocity * Time.fixedDeltaTime);
        }
    }
}
