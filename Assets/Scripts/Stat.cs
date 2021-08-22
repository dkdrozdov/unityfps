using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;
public class Stat : NetworkBehaviour
{
    public NetworkVariableFloat currentValue = new NetworkVariableFloat(new NetworkVariableSettings
    {
        WritePermission = NetworkVariablePermission.Everyone
    });
    public float maxValue = 100f;
    public delegate void ValueChange(float newValue);
    public event ValueChange OnValueChange;

    protected void OnChange(float newValue)
    {
        OnValueChange?.Invoke(newValue);
    }
    public float GetMaxValue()
    {
        return maxValue;
    }
    public float GetValue()
    {
        return currentValue.Value;
    }
    public void SetMaxValue(float value)
    {
        maxValue = value;
    }
    public void SetValue(float value)
    {
        currentValue.Value = value;
    }

}
