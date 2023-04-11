using UnityEngine;

namespace Assets.Scripts
{
    internal interface Damageable
    {
        void TakeDamage(float damage, AttackBase attackType, GameObject damager);
    }
}