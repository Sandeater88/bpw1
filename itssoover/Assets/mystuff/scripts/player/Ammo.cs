using UnityEngine;
using UnityEngine.UI;

public class Ammo: MonoBehaviour
{
    public Image fillImage; // Reference to the fill image of the health bar
    private float currentFill = 1f; // Current fill amount of the health bar, initially full

    void Start()
    {
        // Set the initial fill amount to full
        fillImage.fillAmount = 1f;
    }

    // Increase the fill amount of the health bar
    public void IncreaseFill(float amount)
    {
        currentFill = Mathf.Clamp01(currentFill + amount);
        fillImage.fillAmount = currentFill;
    }

    // Decrease the fill amount of the health bar
    public void DecreaseFill(float amount)
    {
        currentFill = Mathf.Clamp01(currentFill - amount);
        fillImage.fillAmount = currentFill;
    }
}
