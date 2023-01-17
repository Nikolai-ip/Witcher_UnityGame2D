using AISystem;
using Assets.Scripts;
using UnityEngine;

public abstract class Enemy : Entity, Damageable
{
    [SerializeField] StanEnemyController _stanController;
    public bool canTakeDamage = true;
    public void TakeDamage(int damage)
    {
        if (canTakeDamage)
        {
            health -= damage;
            _stanController.Stan();
            animator.SetTrigger(takeDamageNameAnimation);
            if (health < 0)
            {
                Die();
            }
        }

    }
}