using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonController : MonoBehaviour
{
    public float walkingMoveSpeed;
    public float runningMoveSpeed;
    public float crouchingMoveSpeed;
    private float currentMoveSpeed = 2f;


    public float mouseSensitivity = 2f;
    public Transform cameraHolder;
    public Animator animator;

    private CharacterController controller;
    private float verticalRotation = 0f;
    private bool isCrouching = false;

    private Vector3 velocity;
    private float gravity = -9.81f;


    // Collider settings
    private readonly float standingHeight = 2f;
    private readonly float crouchingHeight = 1.2f;
    private Vector3 standingCameraPosition = new (0f, -0.2f, 0.2f);
    private Vector3 crouchingCameraPosition = new (0f, -0.8f, 0.2f);

    private Vector3 standingCenterPosition = new (0f, -0.8f, 0.0f);
    private Vector3 crouchingCenterPosition = new (0f, -1.1f, 0.0f);


    void Start()
    {
        controller = GetComponent<CharacterController>();

        Walk();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Run()
    {
        currentMoveSpeed = runningMoveSpeed;
    }

    void Walk()
    {
        currentMoveSpeed = walkingMoveSpeed;
    }

    void Update()
    {
        // Movimento
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        controller.Move(currentMoveSpeed * Time.deltaTime * move);

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);

        // Aggiorna parametro Speed e IsCrounching per animazioni
        float currentSpeed = new Vector3(controller.velocity.x, 0, controller.velocity.z).magnitude;
        animator.SetFloat("Speed", currentSpeed);
        animator.SetBool("IsCrouching", isCrouching);

        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching;

            if (isCrouching)
            {
                controller.height = crouchingHeight;
                controller.center = crouchingCenterPosition;
                currentMoveSpeed = crouchingMoveSpeed;
            }
            else
            {
                controller.height = standingHeight;
                controller.center = standingCenterPosition;
                currentMoveSpeed = walkingMoveSpeed;
            }
        }

        
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching)
        {
            Run();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
        {
            Walk();
        }

        // Mouse Look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 50f);
        cameraHolder.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        
        Vector3 targetPosition = isCrouching ? crouchingCameraPosition : standingCameraPosition;
        cameraHolder.localPosition = Vector3.Lerp(cameraHolder.localPosition, targetPosition, Time.deltaTime * 10f);
    }
}