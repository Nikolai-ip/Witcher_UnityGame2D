using Assets.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private AnimatorAttackController _animatorAttack;
    [SerializeField] private float _attackTimeBonuse;
    [SerializeField] private float _succsessTimeBetweenAttack;
    [SerializeField] private float _failedTimeBetweenAttack;
    private AttackMode _attackMode;
    private PlayerMove _playerMove;
    private HitController _hitController;
    private bool _canAttack;

    public event EventHandler<bool> onCanAttack;

    public bool CanAttack
    {
        get { return _canAttack; }
        set
        {
            _canAttack = value;
            onCanAttack?.Invoke(this, _canAttack);
        }
    }

    private List<List<Attack>> _attackCombinations = new List<List<Attack>>()
    {
        new List<Attack>(){ new LeftAttack(), new RightAttack(), new Pirouette()},
        new List<Attack>(){ new RightAttack(), new Pirouette(), new LeftAttack()},
        new List<Attack>(){  new Pirouette(), new LeftAttack(), new RightAttack()},
    };

    [SerializeField] private List<Attack> _currentAttackCombination = new List<Attack>();

    private void Start()
    {
        _playerMove = GetComponent<PlayerMove>();
        _animatorAttack = GetComponent<AnimatorAttackController>();
        _hitController = GetComponent<HitController>();
        _attackMode = GetComponent<AttackMode>();
        CanAttack = true;
    }

    public void Attack(Attack attackType)
    {
        _currentAttackCombination.Add(attackType);
        _attackTimeBonuse = DefineAttackTimeBonusType();
        _attackMode.TurnOn();
        if (CanAttack)
        {
            CanAttack = false;
            _hitController.CheckForHit();
            _animatorAttack.PlayAnimationWithDuration(attackType.AnimationName, attackType.AnimationDuration + _attackTimeBonuse);
            _playerMove.MoveByDirection(attackType.MovePlayerTime, attackType.MovePlayerSpeed);
        }
    }

    private float DefineAttackTimeBonusType()
    {
        if (IsSuccsessCombo())
        {
            return _succsessTimeBetweenAttack;
        }
        else
        {
            _currentAttackCombination.Clear();
            return _failedTimeBetweenAttack;
        }
    }

    private bool IsSuccsessCombo()
    {
        foreach (var combinations in _attackCombinations)
        {
            if (combinations.ContainsList(_currentAttackCombination))
            {
                return true;
            }
        }
        _currentAttackCombination.Clear();
        return false;
    }
}