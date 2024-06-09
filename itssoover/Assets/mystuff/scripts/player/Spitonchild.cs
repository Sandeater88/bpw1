using UnityEngine;

public class Spitonchild : MonoBehaviour
{
    public GameObject bulletPrefab; // Bullet prefab to be instantiated
    public float shootingForce = 10f; // Force applied to the bullet
    public Transform shootPoint; // The point from where the bullet will be shot
    private Vector3 originalShootPointPosition; // The original position of the shoot point

    void Start()
    {
        // Store the original position of the shoot point
        originalShootPointPosition = shootPoint.localPosition;
    }

    void Update()
    {
        // Update direction to face the mouse cursor
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - shootPoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the shoot point to face the mouse cursor
        shootPoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Adjust the shoot point position if "A" key is pressed
        if (Input.GetKeyDown(KeyCode.A))
        {
            shootPoint.localPosition -= new Vector3(4f, 0f, 0f);
        }

        // Reset the shoot point position if "D" key is pressed
        if (Input.GetKeyDown(KeyCode.D))
        {
            shootPoint.localPosition = originalShootPointPosition;
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
