using System;
using UnityEngine;
using System.Threading.Tasks;

namespace StateMachineAI
{
    internal class EscapeState : State
    {

        public override void UpdateLogic()
        {

        }

        public override async void UpdatePhysics()
        {
            Vector2 moveX = new Vector2(-MathF.Sign(GetDistanceToPlayerWithSign()) * stateMachine.escapeVelocity, 0);
            stateMachine.Rb.velocity = moveX;
            await Task.Delay((int)(stateMachine.escapeMoveDuration * 1000));
            stateMachine.Rb.velocity = Vector2.zero;
            stateMachine.ChangeState(stateMachine.GetBaseState());
        }
        public EscapeState(StateMachine stateMachine) : base(stateMachine)
        {
        }

    }
}
