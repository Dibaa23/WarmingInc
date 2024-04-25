using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public Image peopleBar;
    public Image economyBar;
    public Image ecosystemBar;

    public float peopleFill = 50f;
    public float economyFill = 50f;
    public float ecosystemFill = 50f;

    public float fluctuationRange = 10f;  // Maximum percentage point change per fluctuation

    void Start()
    {
        UpdateBarDisplays();
        InvokeRepeating("FluctuateStats", 1f, 0.5f);  // Start after 2 seconds and repeat every 5 seconds
    }

    void FluctuateStats()
    {
        // Randomly adjust each stat by a value in the range of -fluctuationRange to fluctuationRange
        AdjustBar(peopleBar, ref peopleFill, Random.Range(-fluctuationRange, fluctuationRange));
        AdjustBar(economyBar, ref economyFill, Random.Range(-fluctuationRange, fluctuationRange));
        AdjustBar(ecosystemBar, ref ecosystemFill, Random.Range(-fluctuationRange, fluctuationRange));
    }

    public void ApplyPolicyEffects(float peopleEffect, float economyEffect, float ecosystemEffect)
    {
        peopleFill += peopleFill * (peopleEffect / 200f);
        economyFill += economyFill * (economyEffect / 200f);
        ecosystemFill += ecosystemFill * (ecosystemEffect / 200f);

        ClampStats();
        UpdateBarDisplays();
    }

    void AdjustBar(Image bar, ref float fill, float amount)
    {
        fill += amount;
        ClampStats();
        UpdateBarDisplays();
    }

    void ClampStats()
    {
        // Ensure values remain within 0 and 100
        peopleFill = Mathf.Clamp(peopleFill, 0, 100);
        economyFill = Mathf.Clamp(economyFill, 0, 100);
        ecosystemFill = Mathf.Clamp(ecosystemFill, 0, 100);
    }

    void UpdateBarDisplays()
    {
        // Update the UI element fill amounts
        peopleBar.fillAmount = peopleFill / 100f;
        economyBar.fillAmount = economyFill / 100f;
        ecosystemBar.fillAmount = ecosystemFill / 100f;
    }
}
