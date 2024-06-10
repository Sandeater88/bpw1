
using UnityEngine;

public class Ebarmanager : MonoBehaviour
{
    public float maxHealth = 100f; // Maximum health of the enemy
    private float currentHealth;

    public GameObject healthBarPrefab; // Prefab for the health bar
    private GameObject healthBarInstance;

    public Sprite crySprite; // Sprite to change to when health is 0
    private SpriteRenderer spriteRenderer;

    public float speedBoost = 3f; // Speed boost when health is 0
    private bool isCrying = false; // Flag to track if enemy is crying

    private void Start()
    {
        currentHealth = maxHealth;
        NotifyHealthChanged();

        // Instantiate the health bar prefab and set its parent to the enemy
        healthBarInstance = Instantiate(healthBarPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        healthBarInstance.transform.SetParent(transform);

        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        NotifyHealthChanged();
        Debug.Log("Enemy health: " + currentHealth); // Print enemy health

        // Check if health is 0
        if (currentHealth <= 0 && !isCrying)
        {
            Cry();
        }
    }

    private void NotifyHealthChanged()
    {
        float healthPercentage = currentHealth / maxHealth;
        if (healthBarInstance != null)
        {
            healthBarInstance.GetComponent<HealthBar>().UpdateFillAmount(healthPercentage);
        }
    }

    private void Cry()
    {
        isCrying = true;

        // Change sprite to "cry" sprite
        spriteRenderer.sprite = crySprite;

        // Boost speed
        GetComponent<EnemyMovement>().speed += speedBoost;

        // Move up
        transform.Translate(Vector3.up * Time.deltaTime * speedBoost);

        // Disable collision
        GetComponent<Collider2D>().enabled = false;
    }
}
