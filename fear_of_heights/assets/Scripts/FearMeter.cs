using UnityEngine;

public class FearMeter : MonoBehaviour
{
    public Transform elevator; // The elevator whose height will influence the fear meter
    public float maxRotationSpeed = 50f; // Maximum speed of rotation
    public float maxElevatorHeight = 10f; // The height at which the elevator reaches maximum rotation speed
    public float maxRotationAngle = 180f; // Maximum angle to rotate (from left to right)

    private float rotationSpeed = 0f;
    private float initialRotationAngle = 0f;

    void Start()
    {
        if (elevator == null)
        {
            Debug.LogError("Elevator not assigned in FearMeterController.");
        }

        // Set the initial rotation of the fear meter to the leftmost position
        initialRotationAngle = transform.rotation.eulerAngles.z;
    }

    void Update()
    {
        // Get the elevator's height
        float elevatorHeight = elevator.position.y;

        // Calculate the speed based on the elevator's height (scaled between 0 and maxRotationSpeed)
        rotationSpeed = Mathf.Clamp(elevatorHeight / maxElevatorHeight, 0f, 1f) * maxRotationSpeed;

        // Check if the elevator has reached its maximum height
        if (elevatorHeight >= maxElevatorHeight)
        {
            rotationSpeed = 0f; // Stop the rotation when the elevator reaches max height
        }

        // Apply the rotation to the parent
        if (rotationSpeed > 0f)
        {
            // Rotate around the pivot point (assumed to be the parent's position)
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }

        // Limit the rotation to the maxRotationAngle (prevent the fear meter from rotating too much)
        float currentRotationAngle = transform.rotation.eulerAngles.z;
        if (currentRotationAngle > initialRotationAngle + maxRotationAngle)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, initialRotationAngle + maxRotationAngle);
        }
    }
}
