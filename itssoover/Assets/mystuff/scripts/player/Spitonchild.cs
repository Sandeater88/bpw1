using UnityEngine;

public class Spitonchild : MonoBehaviour
{
    public GameObject bulletPrefab; // Bullet prefab to be instantiated
    public float shootingForce = 10f; // Force applied to the bullet
    public Transform shootPoint; // The point from where the bullet will be shot
    public Transform playerTransform; // Reference to the player's transform

    void Update()
    {
        // Update direction to face the mouse cursor
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - shootPoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the shoot point to face the mouse cursor
        shootPoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Mirror the player sprite if the cursor is on the left side of the player or if "A" key is pressed
        if (mousePosition.x < playerTransform.position.x || Input.GetKey(KeyCode.A))
        {
            playerTransform.localScale = new Vector3(-1, 1, 1); // Mirror the player sprite
        }
        else if (mousePosition.x >= playerTransform.position.x || Input.GetKey(KeyCode.D))
        {
            playerTransform.localScale = new Vector3(1, 1, 1); // Reset to normal
        }

        // Shoot a bullet when "F" key is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            Shoot(direction);
        }
    }

    void Shoot(Vector2 direction)
    {
        // Spawn the bullet at the shooting point
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

        // Apply force to the bullet
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * shootingForce, ForceMode2D.Impulse);
    }
}
