using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Otherammo healthBar; // Reference to the Otherammo script attached to the health bar
    public GameObject pooPrefab; // Prefab for the object to be spawned when pressing Q
    public AudioClip pooSound; // Sound effect for dropping poo
   

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            EatApple();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropPoo();
        }
    }

    private void EatApple()
    {
        // Find the apple GameObject with the "Apple" tag
        GameObject apple = GameObject.FindGameObjectWithTag("Apple");
        if (apple != null)
        {
            // Destroy the apple GameObject
            Destroy(apple);

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
}
