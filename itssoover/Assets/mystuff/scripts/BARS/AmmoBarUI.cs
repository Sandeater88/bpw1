using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    public Image ammoFill;
    public Spitonchild spitonchild; // Reference to the Spitonchild script

    public Color fullColor = Color.blue;
    public Color midColor = Color.cyan;
    public Color lowColor = Color.gray;
    public float midAmmoThreshold = 0.5f;
    public float lowAmmoThreshold = 0.25f;

    private void OnEnable()
    {
        spitonchild.OnAmmoChanged += UpdateAmmoFill;
    }

    private void OnDisable()
    {
        spitonchild.OnAmmoChanged -= UpdateAmmoFill;
    }

    private void UpdateAmmoFill(float ammoPercentage)
    {
        ammoFill.fillAmount = ammoPercentage;

        // Change color based on ammo percentage
        if (ammoPercentage <= lowAmmoThreshold)
        {
            ammoFill.color = lowColor;
        }
        else if (ammoPercentage <= midAmmoThreshold)
        {
            ammoFill.color = midColor;
        }
        else
        {
            ammoFill.color = fullColor;
        }
    }
}
