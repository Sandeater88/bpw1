using UnityEngine;
using UnityEngine.UI;

public class Otherammo : MonoBehaviour
{
    public Image healthFill;
    private float maxHealth = 100f; // Maximum health
    private float currentHealth; // Current health

    void Start()
    {
        SetMaxHealth();
    }

    public void SetMaxHealth()
    {
        currentHealth = 0f; // Set current health to zero initially
        UpdateHealthBar();
    }

    public void SetHealth(float health)
    {
        currentHealth = Mathf.Clamp(health, 0f, maxHealth);
        UpdateHealthBar();
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    private void UpdateHealthBar()
    {
        float healthPercentage = currentHealth / maxHealth;
        healthFill.fillAmount = healthPercentage;
    }
}
