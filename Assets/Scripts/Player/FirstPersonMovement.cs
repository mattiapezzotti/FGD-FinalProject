using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    // Movements
    public float walkMoveSpeed, runMoveSpeed, crouchMoveSpeed, crouchRunMoveSpeed;
    private float currentMoveSpeed;
    public float mouseSensitivity = 2f;
    private float verticalRotation = 0f;
    private bool isCrouching = false;
    private float moveX, moveZ, mouseX, mouseY, currentSpeed;

    // Dependencies
    public GameObject character;
    public Transform cameraHolder;
    private Animator animator;
    private CharacterController controller;
    
    // Gravity
    private Vector3 velocity;
    private readonly float gravity = -9.81f;

    // Collider
    public float standingHeight, crouchingHeight;
    private Vector3 standingCameraPosition = new (0f, 1.5f, 0.2f);
    private Vector3 crouchingCameraPosition = new (0f, 1f, 0.2f);
    private Vector3 standingCenterPosition = new (0f, 0.9f, 0.0f);
    private Vector3 crouchingCenterPosition = new (0f, 0.6f, 0.0f);


    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = character.GetComponent<Animator>();

        isCrouching = false;
        ToWalkState();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void CheckState()
    {
        animator.SetBool("IsCrouching", isCrouching);

        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (isCrouching)
            {
                ToWalkState();
            }
            else
            {
                ToCrouchState();
            }
        }

        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (isCrouching)
            {
                ToCrouchRunState();
            }
            else
            {
                ToRunState();
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (isCrouching)
            {
                ToCrouchState();
            }
            else
            {
                ToWalkState();
            }
        }
    }

    void ToCrouchState()
    {
        controller.height = crouchingHeight;
        controller.center = crouchingCenterPosition;
        currentMoveSpeed = crouchMoveSpeed;
        isCrouching = true;
    }

    void ToCrouchRunState()
    {
        currentMoveSpeed = crouchRunMoveSpeed;
    }

    void ToRunState()
    {
        currentMoveSpeed = runMoveSpeed;
    }

    void ToWalkState()
    {
        controller.height = standingHeight;
        controller.center = standingCenterPosition;
        currentMoveSpeed = walkMoveSpeed;
        isCrouching = false;
    }

    void Move()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        Vector3 horizontalMove = move * currentMoveSpeed;

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        Vector3 totalMove = horizontalMove;
        totalMove.y = velocity.y;

        controller.Move(totalMove * Time.deltaTime);

        Vector3 horizontalVelocity = new (controller.velocity.x, 0, controller.velocity.z);
        currentSpeed = horizontalVelocity.magnitude;
        animator.SetFloat("Speed", currentSpeed);
    }

    void MouseLook()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -48f, 75f);
        cameraHolder.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        
        Vector3 targetPosition = isCrouching ? crouchingCameraPosition : standingCameraPosition;
        cameraHolder.localPosition = Vector3.Lerp(cameraHolder.localPosition, targetPosition, Time.deltaTime * 10f);
    }

    void Update()
    {
        Move();
        MouseLook();
        CheckState();
    }
}