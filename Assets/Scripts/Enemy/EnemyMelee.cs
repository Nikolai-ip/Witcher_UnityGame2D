using AISystem;

public class EnemyMelee : Enemy
{
    protected override void Die()
    {
        var aiStateMachine = GetComponent<AIStateMachine>();
        aiStateMachine.enabled = false;
        aiStateMachine.canAttack = false;
        aiStateMachine.DisableAllStates();
        base.Die();
    }
}
