using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider slider;

    public static HealthBar Instance;
    
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        
        slider = GetComponent<Slider>();
    }

    public void SetMaxHealth(int value)
    {
        slider.maxValue = value;
        slider.value = value;
    }

    public void SetHealth(int value)
    {
        slider.value = value;
    }
}
