using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareCrow : Entity, Damageable
{
    private Animator _animator;
    private void Start()
    {
        _animator= GetComponent<Animator>();
    }
    public void TakeDamage(int damage)
    {
        _health-=damage;
        _animator.SetTrigger("BeatByPlayer");
        if (_health < 0)
        {
            Die();
        }
    }
}
