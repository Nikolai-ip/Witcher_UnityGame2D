namespace StateMachineAI
{
    public class IdleState : State
    {
        public override void UpdateLogic()
        {
            if (GetDistanceToPlayer() < stateMachine.DistanceToMove)
            {
                stateMachine.ChangeState(stateMachine.Move);
            }
        }

        public override void UpdatePhysics()
        {
        }

        public IdleState(StateMachine stateMachine) : base(stateMachine)
        {
        }
    }
}