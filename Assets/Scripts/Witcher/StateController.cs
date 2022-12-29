using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Witcher
{
    internal class StateController:MonoBehaviour
    {
        private StateType _currentState;

        public void SetState(StateType state)
        {
            _currentState = state;
        }
    }
}
