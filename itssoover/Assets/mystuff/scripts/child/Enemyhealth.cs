using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Image healthFill; // Reference to the fill image of the health bar
    public Ebarmanager enemy; // Reference to the enemy script

    public Color fullColor = Color.green;
    public Color midColor = Color.yellow;
    public Color lowColor = Color.red;
    public float midHealthThreshold = 0.5f;
    public float lowHealthThreshold = 0.25f;

    private void OnEnable()
    {
        enemy.OnHealthChanged += UpdateHealthFill;
    }

    private void OnDisable()
    {
        enemy.OnHealthChanged -= UpdateHealthFill;
    }

    private void UpdateHealthFill(float healthPercentage)
    {
        healthFill.fillAmount = healthPercentage;

        // Change color based on health percentage
        if (healthPercentage <= lowHealthThreshold)
        {
            healthFill.color = lowColor;
        }
        else if (healthPercentage <= midHealthThreshold)
        {
            healthFill.color = midColor;
        }
        else
        {
            healthFill.color = fullColor;
        }
    }

    private void LateUpdate()
    {
        // Make the health bar face the camera
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0); // Correct the rotation
    }
}
