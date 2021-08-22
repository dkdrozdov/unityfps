using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : Health
{
    private StatBar hpBar;
    protected override void Start()
    {
        base.Start();
        hpBar.SetMaxValue(maxValue);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10f);
        }
    }
    override public void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        hpBar.SetValue(currentValue.Value);
    }

    public void SetStatBar(StatBar bar)
    {
        hpBar = bar;
    }
}
