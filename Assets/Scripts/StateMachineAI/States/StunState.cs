using System.Threading.Tasks;
using UnityEngine;

namespace StateMachineAI
{
    [System.Serializable]
    public abstract class StunState : State
    {
        [SerializeField] protected float stunDuration;
        [SerializeField] protected bool stunned = false;
        protected string animationName;
        public override void UpdateLogic()
        {
            if (!stunned)
            {
                stunned = true;
                stateMachine.IsStuned = true;
                stateMachine.Animator.SetTrigger(animationName);
                stateMachine.Animator.SetBool(animationName+"Idle", stunned);
                Stun();
            }
        }

        protected async void Stun()
        {
            await Task.Delay((int)(stunDuration * 1000));
            stunned = false;
            stateMachine.IsStuned = false;
            stateMachine.Animator.SetBool(animationName + "Idle", stunned);
            stateMachine.ChangeState(stateMachine.GetBaseState());
        }

        public override void UpdatePhysics()
        {
        }

        public StunState(StateMachine stateMachine) : base(stateMachine)
        {
        }
    }
}