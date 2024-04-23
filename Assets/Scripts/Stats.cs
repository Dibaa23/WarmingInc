using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Stats : MonoBehaviour
{
    // References to the UI Images that represent the bars
    public Image peopleBar;
    public Image economyBar;
    public Image ecosystemBar;

    // The current fill levels for each bar
    public float peopleFill = 100f;
    public float economyFill = 100f;
    public float ecosystemFill = 100f;

    void Start()
    {
        // Set the initial fill amount for each UI bar
        peopleBar.fillAmount = peopleFill / 100f;
        economyBar.fillAmount = economyFill / 100f;
        ecosystemBar.fillAmount = ecosystemFill / 100f;
    }

    void Update()
    {
        // Check if the space bar is pressed
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            // Adjust the fill level of each bar by a random amount
            AdjustBar(peopleBar, ref peopleFill, Random.Range(-10f, 10f));
            AdjustBar(economyBar, ref economyFill, Random.Range(-10f, 10f));
            AdjustBar(ecosystemBar, ref ecosystemFill, Random.Range(-10f, 10f));
        }
    }

    // General method to adjust the fill level of a bar
    void AdjustBar(Image bar, ref float fill, float amount)
    {
        fill += amount;
        fill = Mathf.Clamp(fill, 0, 100); // Ensure fill stays within 0-100
        bar.fillAmount = fill / 100f; // Update the UI element

        if (fill <= 0)
        {
            Debug.Log(bar.name + " bar depleted!");
        }
    }
}
