using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace AISystem
{
    public class AIStateMachine : MonoBehaviour
    {
        [SerializeField] private float _attackDistance;
        [SerializeField] private float _boxColliderSize;

        private Dictionary<StateType, State> _statesMap;
        [SerializeField] private Player _player;
        [SerializeField] private int _flipDelayMs;
        [SerializeField] private int _successEscapeChance;
        [SerializeField] private float _maxDistantForEscape;
        private bool _isCanFlip = true;
        private bool _playerLookAtIt;
        private bool _playerOnRight;
        public bool canAttack = true;
        [SerializeField] private float _distanceForPlayer;
        [SerializeField] private float _cantDamageModeDuration;
        private Enemy _it;
        private void Start()
        {
            InitDictionaty();
            _it= GetComponent<Enemy>();
            _statesMap[StateType.Follow].Enable();
            if (_player == null)
                _player = FindObjectOfType<Player>();
            _player.GetComponent<HitController>().onHit += TurnOffEscapeState;
        }

        private void TurnOffEscapeState()
        {
            bool isSuccessEscape = new System.Random().Next(0, 100) <= _successEscapeChance;
            if (isSuccessEscape && _playerLookAtIt && _maxDistantForEscape>=_distanceForPlayer)
            {
                _it.canTakeDamage = false;
                _statesMap[StateType.Escape].Enable();
                Invoke("EnableCanTakeDAmage", _cantDamageModeDuration);
            }
        }
        private void EnableCanTakeDAmage()
        {
            _it.canTakeDamage = true;
        }

        private void InitDictionaty()
        {
            _statesMap = new Dictionary<StateType, State>()
            {
                {StateType.Follow, GetComponent<FollowForPlayerState>() },
                {StateType.Attack, GetComponent<AttackState>() },
                {StateType.Escape, GetComponent<EscapeState>() },
            };
        }

        private void DisableAllStatesBesides(StateType state)
        {
            foreach (var item in _statesMap)
            {
                if (item.Key != state)
                    item.Value.Disable();
            }
        }
        public void DisableAllStates()
        {
            foreach (var item in _statesMap)
            {
                item.Value.Disable();
            }

        }
        private void Update()
        {
            if (_player != null)
            {
                PlayerDirectionCheck();
                _distanceForPlayer = (float)Math.Sqrt(Math.Pow(Math.Abs(_player.transform.position.x - transform.position.x), 2) + Math.Pow(Math.Abs(_player.transform.position.y - transform.position.y), 2));
                if (_isCanFlip)
                {
                    FlipCheck();
                }
                if (canAttack)
                    CheckCanAttack();
            }
        }
        private void PlayerDirectionCheck()
        {
            if (Mathf.Sign(_player.transform.localScale.x) != Mathf.Sign(transform.localScale.x))
            {
                _playerLookAtIt = true;
            }
            else
            {
                _playerLookAtIt = false;
            }
        }



        private async void FlipCheck()
        {
            if ((_player.transform.position.x - transform.position.x) < 0)
            {
                _playerOnRight = false;
            }
            else
            {
                _playerOnRight = true;
            }
            if (!_playerOnRight && transform.localScale.x > 0 || _playerOnRight && transform.localScale.x < 0)
            {
                _isCanFlip = false;
                await Task.Delay(_flipDelayMs);
                Flip();
                _isCanFlip = true;
            }
        }

        private void Flip()
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
        private void CheckCanAttack()
        {
            if ((_attackDistance + _boxColliderSize) >= _distanceForPlayer)
            {
                _statesMap[StateType.Attack].Enable();
                DisableAllStatesBesides(StateType.Attack);
            }
            else
            {
                _statesMap[StateType.Follow].Enable();
            }
        }

        private void OnDrawGizmos()
        {
            if (_player != null)
            {
                Gizmos.DrawLine(transform.position, (Vector2)transform.position + new Vector2(Mathf.Sign(_player.transform.position.x - transform.position.x), 0) * _attackDistance);
            }
        }
    }
}