using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float mouseSensitivity = 2.5f;
    public Transform cameraTransform;
    public float climbSpeed = 2f;

    private Rigidbody rb;
    private bool isClimbing = false;
    private bool isRopeClimbing = false; // Track rope climbing state

    private float verticalRotation = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.freezeRotation = true; // Prevent unintentional physics-based rotation
        }
    }

    void Update()
    {
        // Handle camera rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(0, mouseX, 0);
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f); // Prevent excessive rotation
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        if (isClimbing || isRopeClimbing)
        {
            HandleClimbing();
        }
        else
        {
            HandleMovement();
        }
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = transform.forward * verticalInput + transform.right * horizontalInput;
        // The player does not jump or interact with the y-axis, so it is kept with a linear velocity
        rb.linearVelocity = new Vector3(movement.x * movementSpeed, rb.linearVelocity.y, movement.z * movementSpeed);
    }

    void HandleClimbing()
    {
        float verticalInput = Input.GetAxis("Vertical");

        if (isRopeClimbing)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, verticalInput * climbSpeed, rb.linearVelocity.z); // Rope-specific climbing
        }
        else
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, verticalInput * climbSpeed, rb.linearVelocity.z); // Ladder climbing
        }

        // Check if the player is at the ground level to exit climbing mode
        if (IsAtGroundLevel())
        {
            ExitClimbingMode();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            EnterClimbingMode();
        }
        else if (other.CompareTag("Rope"))
        {
            EnterRopeClimbingMode();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder") && !IsAtGroundLevel())
        {
            ExitClimbingMode();
        }
        else if (other.CompareTag("Rope"))
        {
            ExitRopeClimbingMode();
        }
    }

    private bool IsAtGroundLevel()
    {
        // Cast a ray slightly below the player's position to check for the ground
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.6f))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                return true;
            }
        }
        return false;
    }

    private void EnterClimbingMode()
    {
        isClimbing = true;
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero; // Reset velocity for smooth climbing
    }

    private void ExitClimbingMode()
    {
        isClimbing = false;
        rb.useGravity = true;
        rb.linearVelocity = Vector3.zero; // Reset velocity for smooth transition to walking
    }

    private void EnterRopeClimbingMode()
    {
        isRopeClimbing = true;
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero; // reset velocity for climbing
    }

    private void ExitRopeClimbingMode()
    {
        isRopeClimbing = false;
        rb.useGravity = true;
        rb.linearVelocity = Vector3.zero; // Reset velocity for smooth transition from rope climbing
    }
}
