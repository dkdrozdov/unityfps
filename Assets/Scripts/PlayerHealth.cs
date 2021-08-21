using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : Health
{
    public StatBar hpBar;
    public PlayerHealth(float max) : base(max) { }
    void Start()
    {
        hpBar.SetMaxValue(maxHealth);
    }
    override public void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        hpBar.SetValue(currentHealth);
    }
}
