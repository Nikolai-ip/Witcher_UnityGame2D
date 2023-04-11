using System;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyMelee : Enemy
{
    protected StateMachine stateMachine;
    private void Start()
    {
        BaseInit();
    }
    protected override void BaseInit()
    {
        base.BaseInit();
        stateMachine = GetComponent<StateMachine>();
    }
    public override void TakeDamage(float damage, AttackBase attackType, GameObject damager)
    {
        if (stateMachine.IsStuned)
            damage *= 2;
        base.TakeDamage(damage, attackType, damager);

        if (canTakeDamage && Health - damage>0)
            stateMachine.ChangeState(stateMachine.TakeDamageStun);
    }

    protected override void Die()
    {
        stateMachine.ChangeState(stateMachine.Die);
    }
    public override void DestroyObject(float delay = 0)
    {
        stateMachine.enabled = false;
        base.DestroyObject(delay);
    }

}