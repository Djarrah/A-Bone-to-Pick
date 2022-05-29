using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBar : HealthBar
{
    [SerializeField] Gradient gradient;
    
    Image fill;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        fill = GetComponentInChildren<Image>();
    }

    public override void SetHealth(int value)
    {
        base.SetHealth(value);

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
