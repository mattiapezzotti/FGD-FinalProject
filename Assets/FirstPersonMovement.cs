using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    public Transform cameraTransform;
    public Animator animator;  // Riferimento all'Animator del personaggio

    private CharacterController controller;
    private float verticalRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Movimento
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        controller.Move(move * moveSpeed * Time.deltaTime);

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

        // Mouse Look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 40f);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
}
