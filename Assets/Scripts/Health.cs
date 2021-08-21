using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    
    public Health(float max)
    {
        maxHealth = max;
        currentHealth = max;
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
    }
    public virtual float GetValue()
    {
        return currentHealth;
    }
}
