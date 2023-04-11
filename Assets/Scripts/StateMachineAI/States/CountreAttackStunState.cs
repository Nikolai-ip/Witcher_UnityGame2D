using System.Threading.Tasks;

namespace StateMachineAI
{
    public class CountreAttackStunState : StunState
    {
        public CountreAttackStunState(StateMachine stateMachine) : base(stateMachine)
        {
            stunDuration = stateMachine.countreAttackStunDuration;
            animationName = "CountreAttackStun";
        }
    }
}