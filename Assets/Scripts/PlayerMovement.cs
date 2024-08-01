using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the capsule movement
    private Rigidbody rb;
    public Transform cameraTransform; // Reference to the camera's transform

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from WASD or arrow keys
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create a movement vector based on camera's forward direction
        Vector3 forward = cameraTransform.forward; // Get the camera's forward direction
        Vector3 right = cameraTransform.right; // Get the camera's right direction

        // Set Y to 0 to ignore vertical movement
        forward.y = 0;
        right.y = 0;

        // Normalize vectors to avoid diagonal speed increase
        forward.Normalize();
        right.Normalize();

        // Create movement vector relative to the camera's direction
        Vector3 movement = (forward * moveVertical + right * moveHorizontal).normalized;

        // Move the capsule
        if (movement.magnitude > 0)
        {
            rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);

            // Rotate the capsule to face the movement direction
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }
}
