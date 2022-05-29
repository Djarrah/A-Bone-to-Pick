using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    protected Slider slider;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        slider = GetComponent<Slider>();
    }

    // sets the slider value to the given value
    public virtual void SetHealth(int value)
    {
        slider.value = value;
    }
}
