using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace StateMachineAI
{
    public class AttackState : State
    {
        private AttackUp _attackUp;
        private AttackRightSide _attackRightSide;
        private AttackLeftSide _attackLeftSide;
        private List<AttackBase> _attackList;

        public event Action onHit;

        public bool CanAttack = true;
        private List<AttackBase> randomAttackList;
        private float _delayBetweenComboAttacks;
        private float _delayBetweenAttack;
        private int _hitComboAmount;
        private int _currentComboIndex = 0;

        public override void UpdateLogic()
        {
            Attack();
            if (GetDistanceToPlayer() > stateMachine.DistanceToAttack)
            {
                stateMachine.ChangeState(stateMachine.Move);
            }
        }

        private void Attack()
        {
            if (_currentComboIndex == 0)
                randomAttackList = _attackList.OrderBy(a => Guid.NewGuid()).ToList();

            if (CanAttack)
            {
                CanAttack = false;
                AttackBase randomAttack = randomAttackList[_currentComboIndex];
                stateMachine.Animator.SetTrigger(randomAttack.AnimationName);
                Hit(randomAttack);
                _currentComboIndex++;
                if (_currentComboIndex == _hitComboAmount)
                {
                    TurnOnCanAttackWithDelay(randomAttack.Duration + _delayBetweenComboAttacks);
                    _currentComboIndex = 0;
                }
                else
                    TurnOnCanAttackWithDelay(randomAttack.Duration + _delayBetweenAttack);
            }
        }

        private async void TurnOnCanAttackWithDelay(float delay)
        {
            await Task.Delay((int)(delay * 1000));
            CanAttack = true;
        }

        private async void Hit(AttackBase attack)
        {
            await Task.Delay((int)(attack.DelayBeforeHit * 1000));
            Collider2D[] colliders = Physics2D.OverlapCircleAll(stateMachine.HitPoint.position, stateMachine.DistanceToAttack / 2);
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out Player player) && collider != stateMachine.Collider)
                {
                    onHit?.Invoke();
                    player.GetComponent<Damageable>().TakeDamage(attack.Damage, attack, stateMachine.gameObject);
                }
            }
        }

        public override void UpdatePhysics()
        {
        }

        public AttackState(StateMachine stateMachine) : base(stateMachine)
        {
            _attackLeftSide = new AttackLeftSide(stateMachine.damageSideAttack, stateMachine.delayBeforeSideAttack);
            _attackRightSide = new AttackRightSide(stateMachine.damageSideAttack, stateMachine.delayBeforeSideAttack);
            _attackUp = new AttackUp(stateMachine.damageUpAttack, stateMachine.delayBeforeUpAttack);
            _attackList = new List<AttackBase>() { _attackRightSide, _attackLeftSide,_attackUp };
            _delayBetweenComboAttacks = stateMachine.delayBetweenComboAttacks;
            _delayBetweenAttack = stateMachine.delayBetweenAttack;
            _hitComboAmount = _attackList.Count;
        }

    }
}