using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {

    }
    
    public void SetMaxValue(float newValue)
    {
        slider.maxValue = newValue;
        SetValue(newValue);
    }
    public void SetValue(float newValue)
    {
        slider.value = newValue;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
