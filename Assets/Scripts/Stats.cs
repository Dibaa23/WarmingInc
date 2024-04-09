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
        // Use the new Input System to check for key presses
        // Assuming 'T' decreases people, 'G' decreases economy, 'B' decreases ecosystem
        // Assuming 'Y' heals people, 'H' heals economy, 'N' heals ecosystem
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            AdjustBar(peopleBar, ref peopleFill, -10);
        }
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            AdjustBar(economyBar, ref economyFill, -10);
        }
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            AdjustBar(ecosystemBar, ref ecosystemFill, -10);
        }
        if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            AdjustBar(peopleBar, ref peopleFill, 10);
        }
        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            AdjustBar(economyBar, ref economyFill, 10);
        }
        if (Keyboard.current.nKey.wasPressedThisFrame)
        {
            AdjustBar(ecosystemBar, ref ecosystemFill, 10);
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
