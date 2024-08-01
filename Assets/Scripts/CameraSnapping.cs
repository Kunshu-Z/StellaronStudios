using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSnapping : MonoBehaviour
{
    public Transform player; // Reference to the player (capsule) transform
    public Vector3 offset; // Offset position for the camera
    public float lookSpeed = 2f; // Speed of the camera rotation

    private float rotationX = 0f; // Store rotation on the X-axis (up and down)
    private float rotationY = 0f; // Store rotation on the Y-axis (left and right)

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Snap camera position to player position with offset
        transform.position = player.position + offset;

        // Mouse input for looking around
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        rotationX -= mouseY; // Invert Y axis
        rotationX = Mathf.Clamp(rotationX, -45f, 45f); // Limit up/down rotation

        rotationY += mouseX;

        // Apply rotation to the camera
        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
    }
}
