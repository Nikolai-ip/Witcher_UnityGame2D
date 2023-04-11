using UnityEngine;

namespace StateMachineAI
{
    [System.Serializable]
    public class MoveState : State
    {
        

        public override void UpdateLogic()
        {
            stateMachine.Animator.SetBool("IsMove", true);
            if (GetDistanceToPlayer() < stateMachine.DistanceToAttack)
            {
                stateMachine.ChangeState(stateMachine.Attack);
                stateMachine.Animator.SetBool("IsMove", false);
            }
            if (GetDistanceToPlayer() > stateMachine.DistanceToMove)
            {
                stateMachine.ChangeState(stateMachine.GetBaseState());
                stateMachine.Animator.SetBool("IsMove", false);
            }
        }

        public override void UpdatePhysics()
        {
            FollowForPlayer();
        }

        private void FollowForPlayer()
        {
            Vector2 moveX = new Vector2(Mathf.Sign(GetDistanceToPlayerWithSign()) * stateMachine.speed, 0);
            rb.velocity = moveX;
        }

        public MoveState(StateMachine stateMachine) : base(stateMachine)
        {
        }
    }
}