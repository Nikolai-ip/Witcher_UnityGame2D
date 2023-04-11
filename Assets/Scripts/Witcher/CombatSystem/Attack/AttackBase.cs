using Assets.Scripts.Witcher.CombatSystem.BlockType;
using System;
using UnityEngine;

[System.Serializable]
public abstract class AttackBase : IComparable
{
    [SerializeField] protected float animationDuration;
    [SerializeField] protected float damage;
    public float Damage => damage;
    public float DelayBeforeHit { get; protected set; }
    public float Duration { get; protected set; }
    public float MovePlayerTime { get; protected set; }
    public float MovePlayerSpeed { get; protected set; } = 7.5f;

    public float StaminaCost { get; protected set; } = 20;
    public string AnimationName { get; protected set; } = "Attack";
    public int AttackIndex { get; protected set; }
    public HitType HitType { get; protected set; }
    public AttackBase()
    {

    }
    public AttackBase(float damage)
    {
        this.damage = damage;
    }
    public AttackBase(float damage, float delayBeforeHit)
    {
        this.damage = damage;
        DelayBeforeHit = delayBeforeHit;
    }
    public int CompareTo(object other)
    {
        return AttackIndex.CompareTo((other as AttackBase).AttackIndex);
    }
}