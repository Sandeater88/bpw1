using UnityEngine;

public class Spitonchild : MonoBehaviour
{
    public GameObject bulletPrefab; // Bullet prefab to be instantiated
    public float shootingForce = 10f; // Force applied to the bullet
    public Transform shootPoint; // The point from where the bullet will be shot
    private Vector3 originalShootPointPosition; // The original position of the shoot point

    public Sprite spitSprite; // The spit sprite to display
    private SpriteRenderer spriteRenderer; // The SpriteRenderer component for the spit sprite
    private bool shouldMirrorSpitSprite = false; // To track if the spit sprite should be mirrored

    public Ammo healthBar; // Reference to the health bar

    private int shotsFired = 0; // Number of shots fired by the player
    private const int maxShots = 20; // Maximum number of shots allowed
    private bool canShoot = true; // Whether the player can shoot

    void Start()
    {
        // Store the original position of the shoot point
        originalShootPointPosition = shootPoint.localPosition;

        // Create a new GameObject for the spit sprite
        GameObject spitSpriteObject = new GameObject("SpitSprite");
        spitSpriteObject.transform.SetParent(transform);
        spitSpriteObject.transform.localPosition = Vector3.zero;

        // Add a SpriteRenderer component to the spit sprite object
        spriteRenderer = spitSpriteObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = spitSprite;
        spriteRenderer.enabled = false; // Initially hide the sprite
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
            shouldMirrorSpitSprite = true;
        }

        // Reset the shoot point position if "D" key is pressed
        if (Input.GetKeyDown(KeyCode.D))
        {
            shootPoint.localPosition = originalShootPointPosition;
            shouldMirrorSpitSprite = false;
        }

        // Shoot a bullet when "F" key is pressed and the player can shoot
        if (Input.GetKeyDown(KeyCode.F) && canShoot)
        {
            Shoot(direction);
            StartCoroutine(ShowSpitSpriteTemporarily());

            // Increase shots fired and check if the player has reached the maximum shots
            shotsFired++;
            if (shotsFired >= maxShots)
            {
                canShoot = false;
            }

            // Decrease health bar fill amount
            healthBar.DecreaseFill(1f / maxShots);
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

    System.Collections.IEnumerator ShowSpitSpriteTemporarily()
    {
        // Show the spit sprite
        spriteRenderer.enabled = true;

        // Check if the spit sprite should be mirrored
        spriteRenderer.flipX = shouldMirrorSpitSprite;

        // Wait for 0.5 seconds
        yield return new WaitForSeconds(0.5f);

        // Hide the spit sprite
        spriteRenderer.enabled = false;
    }
}
