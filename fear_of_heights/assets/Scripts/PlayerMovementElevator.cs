using UnityEngine;

public class PlayerMovementElevator : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float mouseSensitivity = 2.5f;
    public Transform cameraTransform;

    private Rigidbody rb;
    private float verticalRotation = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.freezeRotation = true; // Prevent unintentional physics-based rotation
        }
    }

    private void Update()
    {
        // Handle camera rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(0, mouseX, 0); // Rotate player horizontally
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f); // Prevent excessive up/down rotation
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        HandleMovement();
    }

    private void HandleMovement()
    {
        // Movement logic
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Get the direction based on player facing
        Vector3 movement = transform.forward * verticalInput + transform.right * horizontalInput;

        // Set player velocity, ignoring vertical (Y) movement for now
        rb.linearVelocity = new Vector3(movement.x * movementSpeed, rb.linearVelocity.y, movement.z * movementSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check for entering the elevator platform
        if (other.CompareTag("ElevatorPlatform"))
        {
            // Optionally: Handle platform-related effects, like disabling gravity or special movement
            rb.useGravity = false;  // Disable gravity while on the elevator
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check for leaving the elevator platform
        if (other.CompareTag("ElevatorPlatform"))
        {
            rb.useGravity = true; // Re-enable gravity once the player exits the elevator
        }
    }

    void FixedUpdate()
    {
        // Ensure the camera doesn't rotate unintentionally due to Rigidbody physics
        rb.angularVelocity = Vector3.zero;
    }
}

