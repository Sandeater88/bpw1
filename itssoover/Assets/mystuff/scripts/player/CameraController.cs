using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Reference to the player object
    public Transform player;

    // Offset of the camera from the player
    public Vector3 offset;

    // Smoothness factor for the camera movement
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        // If the player exists
        if (player != null)
        {
            // Desired position of the camera
            Vector3 desiredPosition = player.position + offset;

            // Smooth the movement
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Update the camera's position
            transform.position = smoothedPosition;
        }
    }
}
