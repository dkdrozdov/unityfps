using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;
public class Stat : NetworkBehaviour
{
    [SerializeField]
    public NetworkVariableFloat currentValue = new NetworkVariableFloat();
    [SerializeField]
    public NetworkVariableFloat maxValue = new NetworkVariableFloat();
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
        Debug.Log("INVOKE!!!!!!!!" + value);
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
        if (IsServer)
        {
            maxValue.Value = value;
        }
        else
        {
            SetMaxValueServerRpc(value);
        }
    }
    public void SetValue(float value)
    {
        if (IsServer)
        {
            currentValue.Value = value;
        }
        else
        {
            SetValueServerRpc(value);
        }
    }
    [ServerRpc(RequireOwnership = false)]
    protected void SetValueServerRpc(float value)
    {
        SetValue(value);
    }
    [ServerRpc(RequireOwnership = false)]
    protected void SetMaxValueServerRpc(float value)
    {
        SetMaxValue(value);
    }
    private void OnEnable()
    {
        currentValue.OnValueChanged += OnChange;
    }
}
