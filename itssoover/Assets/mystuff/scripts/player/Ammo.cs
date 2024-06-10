using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{
    public Image barImage; // Reference to the Image component representing the empty bar
    public Image fillImage; // Reference to the Image component representing the fill of the bar

    private float currentFill = 1f; // Current fill amount of the health bar, initially full

    void Start()
    {
        // Set the initial fill amount to full
        fillImage.fillAmount = 1f;
    }

    // Decrease the fill amount of the health bar
    public void DecreaseFill(float amount)
    {
        currentFill = Mathf.Clamp01(currentFill - amount);
        fillImage.fillAmount = currentFill;
    }
}
