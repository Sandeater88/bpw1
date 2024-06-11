using UnityEngine;

public class Destroy : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.R))
        {
            Destroy(gameObject); // Destroy the apple when the player presses R while collided
        }
    }
}
