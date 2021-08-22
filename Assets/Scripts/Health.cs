using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;

public class Health : Stat
{

    protected virtual void Start()
    {
        currentValue.Value = maxValue;
    }

    public virtual void TakeDamage(float damage)
    {
        currentValue.Value -= damage;
        currentValue.Value = Mathf.Clamp(currentValue.Value, 0f, maxValue);
        base.OnChange(currentValue.Value);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10f);
        }
    }
}
