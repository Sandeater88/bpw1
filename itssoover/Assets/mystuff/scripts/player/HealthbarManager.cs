using UnityEngine;
using UnityEngine.SceneManagement; // For scene management
using System.Collections; // For IEnumerator

public class HealthBarManager : MonoBehaviour
{
    public float maxHealth = 100f; // Maximum health of the player
    private float currentHealth;

    public GameObject healthBarPrefab; // Prefab for the health bar
    private GameObject healthBarInstance;
    private HealthBar healthBarScript;

    private Vector3 healthBarOffset = new Vector3(0, 2f, 0); // Offset to position the health bar above the player
    private Coroutine damageCoroutine;
    private GameObject currentFlower;

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
        // Check if the player presses "R" and can heal
        if (currentFlower != null && Input.GetKeyDown(KeyCode.R))
        {
            Heal(20f);
            Destroy(currentFlower);
            currentFlower = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("peasant"))
        {
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(TakeDamageOverTime(5f));
            }
        }

        if (collision.CompareTag("Flower"))
        {
            currentFlower = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("peasant"))
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }

        if (collision.CompareTag("Flower"))
        {
            if (collision.gameObject == currentFlower)
            {
                currentFlower = null;
            }
        }
    }

    private IEnumerator TakeDamageOverTime(float damage)
    {
        while (true)
        {
            TakeDamage(damage);
            yield return new WaitForSeconds(1f);
        }
    }

    private void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            LoadGameOverScene();
        }
    }

    private void Heal(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
    }

    private void UpdateHealthBar()
    {
        if (healthBarScript != null)
        {
            float healthPercentage = currentHealth / maxHealth;
            healthBarScript.UpdateHealthBar(healthPercentage);
        }
    }

    private void UpdateHealthUI()
    {
        if (healthBarScript != null)
        {
            float healthPercentage = currentHealth / maxHealth;
            healthBarScript.UpdateHealthBar(healthPercentage);
        }
    }

    private void LoadGameOverScene()
    {
        SceneManager.LoadScene(3); // Load scene number 2
    }
}
