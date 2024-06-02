using System.Collections; // Import the System.Collections namespace
using System.Collections.Generic; // Import the System.Collections.Generic namespace
using UnityEngine; // Import the UnityEngine namespace
using UnityEngine.UI; // Import the UnityEngine.UI namespace

public class Healthbar : MonoBehaviour
{
    public Slider slider; // Reference to the UI Slider component representing the health bar

    // Method to set the maximum health value of the slider
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health; // Set the maximum value of the slider to the given health
        slider.value = health; // Set the current value of the slider to the given health
    }

    // Method to update the health value of the slider
    public void SetHealth(int health)
    {
        slider.value = health; // Set the current value of the slider to the given health
    }
}
