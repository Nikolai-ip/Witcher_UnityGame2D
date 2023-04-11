using Assets.Scripts;
using System;
using UnityEngine;

public abstract class Enemy : Entity, Damageable
{
    public bool canTakeDamage = true;
    public event Action OnTakeDamage;

    public virtual void TakeDamage(float damage, AttackBase attackType, GameObject damager)
    {
        if (canTakeDamage)
        {
            OnTakeDamage?.Invoke();
            Health -= damage;
            if (Health <= 0)
            {
                Die();
            }
        }
    }
}