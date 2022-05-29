using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBar : MonoBehaviour
{
    [SerializeField] Gradient gradient;
    
    Slider slider;
    Image fill;

    public static ShieldBar Instance;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        slider = GetComponent<Slider>();
        fill = GetComponentInChildren<Image>();
    }

    public void SetMaxHealth(int value)
    {
        slider.maxValue = value;
        slider.value = value;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int value)
    {
        slider.value = value;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
