using UnityEngine;

public class EatApple : MonoBehaviour
{
    public Otherammo healthBar; // Reference to the Otherammo script attached to the health bar
    public GameObject pooPrefab; // Prefab for the object to be spawned when pressing Q
    public AudioClip pooSound; // Sound effect for dropping poo

    private GameObject currentApple; // The apple the player is currently colliding with

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            EatTheApple();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropPoo();
        }
    }

    private void EatTheApple()
    {
        if (currentApple != null)
        {
            // Destroy the apple GameObject
            Destroy(currentApple);
            currentApple = null;

            // Increase health by 20f
            healthBar.SetHealth(healthBar.GetHealth() + 20f);
        }
    }

    private void DropPoo()
    {
        if (healthBar.GetHealth() > 0)
        {
            // Decrease health by 20f
            healthBar.SetHealth(healthBar.GetHealth() - 20f);

            // Spawn a GameObject at the player's position
            Instantiate(pooPrefab, transform.position, Quaternion.identity);

            // Play the sound effect
            if (pooSound != null)
            {
                AudioSource.PlayClipAtPoint(pooSound, transform.position);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Apple"))
        {
            currentApple = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Apple"))
        {
            currentApple = null;
        }
    }
}
