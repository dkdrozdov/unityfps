using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;
public class Stat : NetworkBehaviour
{
    [SerializeField]
    protected NetworkVariableFloat currentValue = new NetworkVariableFloat(new NetworkVariableSettings
    {
        WritePermission = NetworkVariablePermission.OwnerOnly,
        ReadPermission = NetworkVariablePermission.Everyone
    });
    [SerializeField]
    protected NetworkVariableFloat maxValue = new NetworkVariableFloat(new NetworkVariableSettings
    {
        WritePermission = NetworkVariablePermission.OwnerOnly,
        ReadPermission = NetworkVariablePermission.Everyone
    });
    public delegate void ValueChange(float newValue);
    public event ValueChange OnValueChange;

    protected void OnChange(float newValue)
    {
        InvokeValueChange(newValue);
    }
    protected void OnChange(float oldValue, float newValue)
    {
        InvokeValueChange(newValue);
    }
    protected void InvokeValueChange(float value)
    {
        OnValueChange?.Invoke(value);
    }
    public float GetMaxValue()
    {
        return maxValue.Value;
    }
    public float GetValue()
    {
        return currentValue.Value;
    }
    public void SetMaxValue(float value)
    {
        maxValue.Value = value;
    }
    public void SetValue(float value)
    {
        currentValue.Value = value;
    }
    private void OnEnable()
    {
        currentValue.OnValueChanged += OnChange;
    }
}
