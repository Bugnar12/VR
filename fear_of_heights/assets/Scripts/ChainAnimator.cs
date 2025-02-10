using UnityEngine;

public class FearMeterSegment : MonoBehaviour
{
    public Transform parentSegment; // The parent segment around which this segment rotates
    public float rotationSpeed = 50f; // Speed at which the segment rotates

    private float initialRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (parentSegment == null)
        {
            parentSegment = transform.parent; // Default to the parent's transform if not set
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (parentSegment != null)
        {
            // Rotate the segment around the parent's axis
            transform.RotateAround(parentSegment.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }

    // Method to adjust the rotation speed based on external factors (e.g., elevator height)
    public void AdjustRotationSpeed(float speed)
    {
        rotationSpeed = speed;
    }
}
