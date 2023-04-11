using UnityEngine;

public class StanEnemyController : MonoBehaviour
{
    private StateMachine _stateMachine;

    private void Start()
    {
        _stateMachine = GetComponent<StateMachine>();
    }

    public void CountreAttackStun()
    {
        _stateMachine.ChangeState(_stateMachine.CountreAttackStun);
    }

    public void BlockStun()
    {
    }
}