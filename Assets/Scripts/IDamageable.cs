using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float value);
    void TakeDamage(float value, float punch, Vector3 source);
    void SetHealthComponent(Health health);
}
