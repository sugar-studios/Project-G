using UnityEngine;
using UnityEngine.SceneManagement;
using ProjectG.Manger;
using UnityEngine.UI;

namespace ProjectG.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public GameObject playerGraphics;
        public GameObject noise;
        public Transform gameCamera;
        public GameManager manager;

        public float crouchSpeed = 5f;
        public float speed = 10f;
        public float sprintSpeed = 15f;
        public float turnSmoothTime = 0.1f;
        public float jumpHeight = 2f;
        public float gravityValue = -19.62f;
        public LayerMask groundMask;
        public float groundDistance = 0.4f;
        public Transform groundCheck;
        public float crouchScale = 0.5f;
        public float scaleTransitionSpeed = 5f;

        public float noiseIntesnity = .5f;

        public Slider healthSlider;
        public float playerHealth;
        public float playerVelo;

        public float currentStamina;
        public float maxStamina = 100f;
        public Animator playerAnimator;
        private float staminaDepletionRate = 10f;
        private float staminaRegainRate = 5f;
        private float staminaRegainDelay = 1.5f;
        private float timeSinceLastStaminaDepletion;

        private CharacterController controller;
        private float turnSmoothVelocity;
        private Vector3 playerVelocity;
        public bool isGrounded;
        private bool shouldJump;
        private bool isCrouching;
        private Vector3 standingScale;
        private float moveSpeed;
        private Vector3 PlayerVelocity;

        private bool isSpeedReduced = false;
        private float speedReductionDuration = 2f;
        private float speedReductionTimer = 0f;

        private void Start()
        {
            healthSlider.value = playerHealth;
            manager.SetMaxStamina(maxStamina, 0f);

            controller = GetComponent<CharacterController>();
            standingScale = playerGraphics.transform.localScale;
            moveSpeed = speed;
            currentStamina = maxStamina;
            UpdateNoiseScale(); // Initial noise scale update
        }

        private void Update()
        {
            playerAnimator.SetFloat("velocityX", PlayerVelocity.x);
            playerAnimator.SetFloat("velocityY", PlayerVelocity.z);

            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask) || controller.isGrounded;

            if (isGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = -2f;
            }

            Move();

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isCrouching && currentStamina >= maxStamina * 0.15f)
            {
                shouldJump = true;
                currentStamina -= maxStamina * 0.05f;
                timeSinceLastStaminaDepletion = 0f;
                UpdateNoiseScale(); // Update noise scale when jumping
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                isCrouching = !isCrouching;
                UpdateNoiseScale(); // Update noise scale when toggling crouch
            }

            Vector3 targetScale = isCrouching ? new Vector3(1, crouchScale, 1) : Vector3.one;
            Vector3 scaledTargetScale = new Vector3(
                standingScale.x * targetScale.x,
                standingScale.y * targetScale.y,
                standingScale.z * targetScale.z
            );

            if (isCrouching)
            {
                controller.height = 1;
                moveSpeed = crouchSpeed;
                staminaRegainRate = 2.5f;
            }
            else
            {
                controller.height = 2;
                staminaRegainRate = 5f;

                if (Input.GetKey(KeyCode.LeftShift) && !isCrouching && currentStamina > 0)
                {
                    moveSpeed = sprintSpeed;
                    currentStamina -= staminaDepletionRate * Time.deltaTime;
                    timeSinceLastStaminaDepletion = 0f;
                    UpdateNoiseScale(); // Update noise scale when sprinting
                }
                else
                {
                    moveSpeed = speed;
                    UpdateNoiseScale(); // Update noise scale when walking or stopping
                }
            }

            playerGraphics.transform.localScale = Vector3.Lerp(playerGraphics.transform.localScale, scaledTargetScale, Time.deltaTime * scaleTransitionSpeed);

            if (isGrounded)
            {
                timeSinceLastStaminaDepletion += Time.deltaTime;
                if (timeSinceLastStaminaDepletion >= staminaRegainDelay)
                {
                    currentStamina += staminaRegainRate * Time.deltaTime;
                    currentStamina = Mathf.Min(currentStamina, maxStamina);
                }
            }

            manager.UpdateStamina(currentStamina);

            if (currentStamina <= 0 && !isSpeedReduced)
            {
                isSpeedReduced = true;
                moveSpeed *= 0.5f;
                speedReductionTimer = speedReductionDuration;
                currentStamina = 0;
                UpdateNoiseScale(); // Update noise scale when fatigued
            }

            if (isSpeedReduced)
            {
                speedReductionTimer -= Time.deltaTime;
                if (speedReductionTimer <= 0)
                {
                    isSpeedReduced = false;
                    moveSpeed = isCrouching ? crouchSpeed : speed;
                    UpdateNoiseScale(); // Reset noise scale after speed reduction ends
                }
            }
        }

        private void FixedUpdate()
        {
            if (shouldJump)
            {
                playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
                shouldJump = false;
            }
            animatePlayer();

            ApplyGravity();
        }

        public void animatePlayer()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + gameCamera.eulerAngles.y;

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                
                PlayerVelocity = moveDir * moveSpeed;

            }
            else
            {
                PlayerVelocity = Vector3.zero;
            }

        }

        void Move()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + gameCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(playerGraphics.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                playerGraphics.transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);

            }
            playerVelo = Mathf.Sqrt(Mathf.Pow(horizontal, 2) + Mathf.Pow(vertical, 2)) * moveSpeed;

        }

        void ApplyGravity()
        {
            playerVelocity.y += gravityValue * Time.fixedDeltaTime;
            controller.Move(playerVelocity * Time.fixedDeltaTime);
        }

        public void updateHealth()
        {
            healthSlider.value = playerHealth;

        }

        void UpdateNoiseScale()
        {
            Vector3 scale;
            if (!isGrounded && moveSpeed !=sprintSpeed)
            {
                noiseIntesnity = 1.75f;
                scale = new Vector3(75, 75, 75);
            }
            else if (isCrouching)
            {
                noiseIntesnity = .5f;
                scale = new Vector3(30, 30, 30);
            }
            else if (moveSpeed == sprintSpeed)
            {
                noiseIntesnity = 2.25f;
                scale = new Vector3(90, 90, 90);
            }
            else if (playerVelo == 0)
            {
                noiseIntesnity = .5f;
                scale = new Vector3(15, 15, 15);
            }
            else
            {
                noiseIntesnity = 1.5f;
                scale = new Vector3(60, 60, 60);
            }
            noise.transform.localScale = scale;
        }
    }
}
