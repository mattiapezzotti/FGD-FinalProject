using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonController : MonoBehaviour
{
    public float walkingMoveSpeed;
    public float runningMoveSpeed;
    public float crouchingMoveSpeed;
    private float currentMoveSpeed = 2f;
    public float mouseSensitivity = 2f;
    public Transform cameraTransform;
    public Animator animator;  // Riferimento all'Animator del personaggio

    private CharacterController controller;
    private float verticalRotation = 0f;

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

        // Aggiorna parametro Speed e IsCrounching per animazioni
        float currentSpeed = new Vector3(controller.velocity.x, 0, controller.velocity.z).magnitude;
        animator.SetFloat("Speed", currentSpeed);
        bool isCrouching = Input.GetKey(KeyCode.C);
        animator.SetBool("IsCrouching", Input.GetKey(KeyCode.C));

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Crouch iniziato");
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            Debug.Log("Crouch terminato");
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Run();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Walk();
        }

        // Mouse Look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 50f);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
}
