using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineAI
{
    public class TakeDamageStunState : StunState
    {
        public TakeDamageStunState(StateMachine statemachine):base(statemachine) 
        {
            stunDuration = stateMachine.takeDamageStunDuration;
            animationName = "TakeDamageStun";
        }
    }
}
