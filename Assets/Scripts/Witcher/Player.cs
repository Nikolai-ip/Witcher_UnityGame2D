using Assets.Scripts;
using UnityEngine;

public class Player : Entity, Damageable
{
    [SerializeField] private StanPlayerController _stanController;

    public bool CanTakeDamage { get ; set ; } = true;

    public void TakeDamage(int damage)
    {
        if (CanTakeDamage)
        {
            health -= damage;
            animator.SetTrigger(takeDamageNameAnimation);
            _stanController.Stan();
            if (health < 0)
            {
                GetComponent<KeyBoardController>().enabled = false;
                Die();
            }
        }
    }
}