using UnityEngine;

public class MoveBetweenPoints : MonoBehaviour
{
    // Array of points (waypoints) for the object to move between
    public Transform[] points;

    // Speed of the object
    public float speed = 5f;

    // Rotation speed of the object
    public float rotationSpeed = 5f;

    // Current target point index
    private int targetPointIndex = 0;

    // Distance threshold for when to consider the object as having "reached" the point
    public float threshold = 0.1f;

    void Update()
    {
        // Check if there are waypoints set
        if (points.Length == 0)
        {
            return;
        } 

        // Get the current target point
        Transform targetPoint = points[targetPointIndex];

        // Move towards the current target point
        MoveTowardsPoint(targetPoint);

        // Rotate towards the target point
        RotateTowardsPoint(targetPoint);

        // Check if the object has reached the target point
        if (Vector3.Distance(transform.position, targetPoint.position) < threshold)
        {
            // Update the target point index to the next one
            targetPointIndex = (targetPointIndex + 1) % points.Length; // Loop back to the start
        }
    }

    // Move the object towards the target point
    private void MoveTowardsPoint(Transform target)
    {
        // Calculate the direction to the target
        Vector3 direction = (target.position - transform.position).normalized;

        // Move the object in that direction
        transform.position += direction * speed * Time.deltaTime;
    }

    // Rotate the object to face the target point
    private void RotateTowardsPoint(Transform target)
    {
        // Calculate the direction to the target
        Vector3 direction = (target.position - transform.position).normalized;

        // Calculate the rotation to look at the target direction
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // Add an offset if your bird model isn't facing the default forward direction (Z-axis)

        Quaternion correction = Quaternion.Euler(0, 180, 0); 
        // Apply the corrected rotation
        lookRotation *= correction;

        // Smoothly rotate towards the target
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

}
