using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public float maxHealth = 100f; // Maximum health of the enemy
    private float currentHealth;

    public GameObject healthBarPrefab; // Prefab for the health bar
    private GameObject healthBarInstance;
    private HealthBar healthBarScript;

    public Sprite crySprite; // Sprite to change to when health is 0
    public float escapeSpeed = 10f; // Speed of the enemy when escaping
    private bool isDead = false;

    private Vector3 healthBarOffset = new Vector3(0, 1.5f, 0); // Offset to position the health bar above the enemy

    private void Start()
    {
        currentHealth = maxHealth;
        Vector3 healthBarPosition = transform.position + healthBarOffset;
        healthBarInstance = Instantiate(healthBarPrefab, healthBarPosition, Quaternion.identity, transform);
        healthBarScript = healthBarInstance.GetComponentInChildren<HealthBar>(); // Ensure to get the component from children
        UpdateHealthBar();
    }

    private void Update()
    {
        if (isDead)
        {
            transform.Translate(Vector3.up * escapeSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("spit"))
        {
            TakeDamage(34f);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        UpdateHealthBar();
        Debug.Log("Enemy Health: " + currentHealth);

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBarScript != null)
        {
            float healthPercentage = currentHealth / maxHealth;
            healthBarScript.UpdateHealthBar(healthPercentage);
        }
    }

    private void Die()
    {
        isDead = true;
        GetComponent<SpriteRenderer>().sprite = crySprite;
        GetComponent<Collider2D>().enabled = false;
        // Optionally disable other components like the enemy AI script
    }
}
