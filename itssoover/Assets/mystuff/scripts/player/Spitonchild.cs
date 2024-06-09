using UnityEngine;

public class Spitonchild : MonoBehaviour
{
    public GameObject bulletPrefab; // Bullet prefab to be instantiated
    public float shootingForce = 10f; // Force applied to the bullet
    public Transform shootPoint; // The point from where the bullet will be shot
    public GameObject arrowPrefab; // Arrow prefab to indicate direction
    private GameObject arrowInstance;

    void Start()
    {
        // Instantiate the arrow at the shooting point's position and set it as a child of the shooting point
        arrowInstance = Instantiate(arrowPrefab, shootPoint.position, Quaternion.identity);
        arrowInstance.transform.SetParent(transform); // Parent the arrow to the player
        PositionArrowAndShootPoint();
    }

    void Update()
    {
        // Update arrow direction to face the mouse cursor
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - shootPoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Clamp the angle to be between -20 and 50 degrees
        angle = Mathf.Clamp(angle, -20f, 50f);

        arrowInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

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

    void PositionArrowAndShootPoint()
    {
        // Adjust shootPoint to be at the end of the arrow
        Vector3 arrowSize = arrowInstance.GetComponent<SpriteRenderer>().bounds.size;
        shootPoint.localPosition = new Vector3(arrowSize.x / 2, 0, 0);
    }
}
