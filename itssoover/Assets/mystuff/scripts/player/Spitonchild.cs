using UnityEngine;
using System.Collections;

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

    public int startingAmmo = 20; // Starting ammo amount
    public int maxAmmo = 20; // Maximum ammo amount
    public float attackCooldown = 1f; // Cooldown duration between attacks
    private int currentAmmo; // Current ammo amount
    private bool canShoot = true; // Whether the player can shoot
    private float lastShootTime; // Time when the player last shot

    public delegate void AmmoChanged(float ammoPercentage);
    public event AmmoChanged OnAmmoChanged;
    public AudioClip spitting;

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

        // Set the sorting order of the spit sprite to a high value
        spriteRenderer.sortingOrder = 10;

        // Set the current ammo to the starting ammo
        currentAmmo = startingAmmo;
        NotifyAmmoChanged();
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

        // Check cooldown
        if (Time.time - lastShootTime < attackCooldown)
        {
            return;
        }

        // Shoot a bullet when "F" key is pressed and the player can shoot
        if (Input.GetKeyDown(KeyCode.F) && canShoot)
        {
            Shoot(direction);
            StartCoroutine(ShowSpitSpriteTemporarily());

            // Decrease current ammo and check if the player has reached the minimum ammo
            currentAmmo--;
            Debug.Log("Current Ammo: " + currentAmmo); // Print the current ammo count

            if (currentAmmo <= 0)
            {
                canShoot = false;
            }

            // Decrease health bar fill amount
            healthBar.DecreaseFill(1f / maxAmmo);

            // Update last shoot time
            lastShootTime = Time.time;

            // Notify ammo change
            NotifyAmmoChanged();
            // Play the sound effect
            if (spitting != null)
            {
                AudioSource.PlayClipAtPoint(spitting, transform.position);
            }
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

    IEnumerator ShowSpitSpriteTemporarily()
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

    void NotifyAmmoChanged()
    {
        if (OnAmmoChanged != null)
        {
            float ammoPercentage = (float)currentAmmo / maxAmmo;
            OnAmmoChanged(ammoPercentage);
        }
    }

    public void IncreaseAmmo(int amount)
    {
        currentAmmo = Mathf.Min(currentAmmo + amount, maxAmmo);
        if (currentAmmo > 0)
        {
            canShoot = true;
        }
        NotifyAmmoChanged();
    }
}
