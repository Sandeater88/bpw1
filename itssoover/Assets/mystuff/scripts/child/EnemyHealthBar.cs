using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Image healthFill;

    public Color fullColor = Color.green;
    public Color midColor = Color.yellow;
    public Color lowColor = Color.red;
    public float midHealthThreshold = 0.5f;
    public float lowHealthThreshold = 0.25f;

    public void UpdateHealthBar(float healthPercentage)
    {
        healthFill.fillAmount = healthPercentage;

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
}
