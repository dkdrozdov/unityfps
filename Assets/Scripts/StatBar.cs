using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    Stat stat;
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
    public void SetStat(Stat s)
    {
        stat = s;
        SetMaxValue(stat.GetMaxValue());
        SetValue(stat.GetValue());
        SubscribeOnValueChange(s);
    }
    void SubscribeOnValueChange(Stat s)
    {
        s.OnValueChange += SetValue;
    }
    private void OnDisable()
    {
        stat.OnValueChange -= SetValue;
    }
}
