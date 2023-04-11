using UnityEngine;

namespace StateMachineAI
{
    [System.Serializable]
    public abstract class State
    {
        protected StateMachine stateMachine;
        private Transform _tr;
        protected Player player;
        protected Rigidbody2D rb;

        public State(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
            player = stateMachine.Player;
            _tr = stateMachine.transform;
            rb = stateMachine.Rb;
        }

        public abstract void UpdateLogic();

        public abstract void UpdatePhysics();

        public float GetDistanceToPlayer()
        {
            return Vector2.Distance(_tr.position, player.transform.position);
        }

        public float GetDistanceToPlayerWithSign()
        {
            return GetDistanceToPlayer() * Mathf.Sign(player.transform.position.x - _tr.position.x);
        }
    }
}