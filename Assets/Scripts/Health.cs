using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;

public class Health : Stat
{
    public virtual void TakeDamage(float damage)
    {
        float newValue = Mathf.Clamp(GetValue() - damage, 0f, GetMaxValue());
        base.SetValue(newValue);
        base.OnChange(newValue);
    }
}
