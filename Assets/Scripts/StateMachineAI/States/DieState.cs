using StateMachineAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

namespace StateMachineAI
{
    public class DieState : State
    {
        private List<string> _animationNames;
        private bool _isDying = false;
        public bool IsDying => _isDying;
        public override async void UpdateLogic()
        {
            if (!_isDying)
            {

                _isDying = true;
                stateMachine.DisableStates();
                stateMachine.CameraShake.ShakeCameraDoubleForce();
                stateMachine.Animator.SetTrigger(_animationNames[UnityEngine.Random.Range(0, _animationNames.Count)]);
                stateMachine.RemoveOnTryHitListener();
                stateMachine.Collider.enabled = false;
                stateMachine.Rb.gravityScale = 0;
                stateMachine.canFlip = false;
                MoveAgainPlayer();
                if (stateMachine.bodyIsDisappearence)
                {
                    await Task.Delay((int)(stateMachine.delayBeforeBodyDisappearence * 1000));
                    await DisappearBody();
                    if (stateMachine.gameObject.TryGetComponent(out Entity entity))
                    {
                        entity.DestroyObject();
                    }
                    stateMachine.Animator.enabled = false;
                }
                DisableEntityComponents();             
            }
        }
        private void DisableEntityComponents()
        {
            UnityEngine.Object.Destroy(stateMachine.EntityUIBars.gameObject);
            UnityEngine.Object.Destroy(stateMachine.gameObject.GetComponent<Entity>());
            UnityEngine.Object.Destroy(stateMachine);
        }
        private async void MoveAgainPlayer()
        {
            stateMachine.Rb.velocity = new Vector2(stateMachine.dieMoveVelocity * -Mathf.Sign(GetDistanceToPlayerWithSign()), 0);
            await Task.Delay((int)(stateMachine.dieMoveDuration * 1000));
            stateMachine.Rb.velocity = Vector2.zero;
        }
        private async Task DisappearBody()
        {
            float time = 0;
            while (stateMachine.SpriteRenderer.color.a > 0)
            {
                time += Time.deltaTime;
                stateMachine.SpriteRenderer.color = new Color(1, 1, 1, 1 - time / stateMachine.disappearenceDuration);
                await Task.Yield();
            }
        }

        public override void UpdatePhysics()
        {
           
        }
        public DieState(StateMachine stateMachine): base(stateMachine) 
        {
            _animationNames = new List<string>() { "DieHead","DieHand" };
        }  
    }
}
