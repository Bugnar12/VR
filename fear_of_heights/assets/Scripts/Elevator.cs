using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform elevatorPlatform; // The platform that will move
    public Transform player; // The player to check for stepping on the elevator
    public Transform basketball; // The basketball object to interact with
    public float liftSpeed = 2f; // Speed at which the elevator moves
    public float liftHeight = 10f; // How high the elevator should rise
    public FearMeterSegment[] fearMeterSegments; // Array of fear meter segments to adjust

    private bool isPlayerOnElevator = false;
    private bool isElevatorRaising = false; // Flag to track if the elevator is raising
    private bool isElevatorAtTop = false; // Flag to check if the elevator has reached the top
    private float initialYPosition; // Starting Y position of the elevator platform
    private Vector3 playerOffset; // The player's offset relative to the elevator
    private Vector3 basketballOffset; // The basketball's offset relative to the elevator
    private Rigidbody basketballRb; // The Rigidbody of the basketball

    // Start is called before the first frame update
    void Start()
    {
        initialYPosition = elevatorPlatform.position.y; // Store the initial position
        basketballRb = basketball.GetComponent<Rigidbody>(); // Get the basketball's Rigidbody
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is on the elevator (this can be a collision or trigger check)
        if (isPlayerOnElevator && !isElevatorRaising)
        {
            isElevatorRaising = true; // Start raising the elevator and the fear meter segments when the player steps on it
        }

        // Move the elevator up
        if (isElevatorRaising && elevatorPlatform.position.y < initialYPosition + liftHeight)
        {
            elevatorPlatform.Translate(Vector3.up * liftSpeed * Time.deltaTime);

            // Move the player with the elevator
            player.position = new Vector3(player.position.x, elevatorPlatform.position.y + playerOffset.y, player.position.z);

            // Move the basketball with the elevator
            basketball.position = new Vector3(basketball.position.x, elevatorPlatform.position.y + basketballOffset.y, basketball.position.z);

            // Adjust the fear meter segments' rotation speed based on the elevator's height
            float normalizedHeight = (elevatorPlatform.position.y - initialYPosition) / liftHeight;
            AdjustRotationSpeedForSegments(normalizedHeight);
        }
        else if (elevatorPlatform.position.y >= initialYPosition + liftHeight - 10f)
        {
            isElevatorAtTop = true;

            // Disable gravity and set Rigidbody to Kinematic to stop it from falling
            basketballRb.useGravity = false;
            basketballRb.isKinematic = true;
        }

        // If the elevator is not raising, ensure basketball is moving with the elevator
        if (isElevatorAtTop)
        {
            // Keep the basketball in the correct position while at the top
            basketball.position = new Vector3(basketball.position.x, elevatorPlatform.position.y + basketballOffset.y, basketball.position.z);
        }
    }

    // Check if the player enters the elevator trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnElevator = true;

            // Store the player's offset from the elevator platform when they step on it
            playerOffset = player.position - elevatorPlatform.position;

            // Store the basketball's offset from the elevator platform when it starts moving
            basketballOffset = basketball.position - elevatorPlatform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnElevator = false;
            isElevatorRaising = false; // Stop the elevator from raising when the player exits
        }
    }

    // Method to adjust rotation speed for each fear meter segment
    private void AdjustRotationSpeedForSegments(float normalizedHeight)
    {
        // Map the normalized height (0 to 1) to a rotation speed
        float rotationSpeed = Mathf.Lerp(50f, 200f, normalizedHeight); // Rotate faster as the elevator rises

        foreach (var segment in fearMeterSegments)
        {
            segment.AdjustRotationSpeed(rotationSpeed); // Apply the calculated speed to each segment
        }
    }
}
