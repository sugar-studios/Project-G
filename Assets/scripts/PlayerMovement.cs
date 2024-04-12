using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public GameObject playerGraphics;
    public Transform camera;
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

    [SerializeField] private float currentStamina;
    public float maxStamina = 100f;
    private float staminaDepletionRate = 10f;
    private float staminaRegainRate = 5f;
    private float staminaRegainDelay = 1.5f;
    private float timeSinceLastStaminaDepletion;

    private CharacterController controller;
    private float turnSmoothVelocity;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private bool shouldJump;
    private bool isCrouching;
    private Vector3 standingScale;
    private float moveSpeed;

    private bool isSpeedReduced = false;
    private float speedReductionDuration = 2f;
    private float speedReductionTimer = 0f;

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        GameObject[] rootObjects = currentScene.GetRootGameObjects();
        manager = rootObjects[0].GetComponent<GameManager>();
        manager.SetMaxStamina(maxStamina, 0f);

        controller = GetComponent<CharacterController>();
        standingScale = playerGraphics.transform.localScale;
        moveSpeed = speed;
        currentStamina = maxStamina;
    }

    private void Update()
    {
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
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching;
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
            }
            else
            {
                moveSpeed = speed;
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
        }

        if (isSpeedReduced)
        {
            speedReductionTimer -= Time.deltaTime;
            if (speedReductionTimer <= 0)
            {
                isSpeedReduced = false;
                moveSpeed = isCrouching ? crouchSpeed : speed;
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
            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
        }
    }

    void ApplyGravity()
    {
        playerVelocity.y += gravityValue * Time.fixedDeltaTime;
        controller.Move(playerVelocity * Time.fixedDeltaTime);
    }
}
