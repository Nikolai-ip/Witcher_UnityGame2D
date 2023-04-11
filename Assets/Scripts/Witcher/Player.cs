using Assets.Scripts;
using UnityEngine;

public class Player : Entity, Damageable
{
    [SerializeField] private StanPlayerController _stanController;
    [SerializeField] private BlockController _blockController;
    public bool CanTakeDamage { get; set; } = true;

    public void TakeDamage(float damage, AttackBase attackType, GameObject damager)
    {
        if (_blockController.EntityBlock(attackType, damager))
            return;
        if (CanTakeDamage)
        {
            Health -= damage;
            animator.SetTrigger(takeDamageNameAnimation);
            _stanController.Stan();
            if (Health < 0)
            {
                Health = MaxHealth;
                //GetComponent<PlayerInputController>().enabled = false;
                //Die();
            }
        }
    }
}