using Assets.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private AnimatorAttackController _animatorAttack;
    private float _attackTimeBonuse;
    [SerializeField] private float _succsessTimeBetweenAttack;
    [SerializeField] private float _failedTimeBetweenAttack;
    private AttackMode _attackMode;
    private PlayerMove _playerMove;
    private HitController _hitController;
    [SerializeField] private bool _canAttack;
    private StaminaController _staminaController;

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

    private List<List<AttackBase>> _attackCombinations = new List<List<AttackBase>>()
    {
        new List<AttackBase>(){ new LeftAttack(), new RightAttack(), new Pirouette()},
        new List<AttackBase>(){ new RightAttack(), new Pirouette(), new LeftAttack()},
        new List<AttackBase>(){  new Pirouette(), new LeftAttack(), new RightAttack()},
    };

    [SerializeField] private List<AttackBase> _currentAttackCombination = new List<AttackBase>();

    private void Start()
    {
        _playerMove = GetComponent<PlayerMove>();
        _staminaController = GetComponent<StaminaController>();
        _animatorAttack = GetComponent<AnimatorAttackController>();
        _hitController = GetComponent<HitController>();
        _attackMode = GetComponent<AttackMode>();
        CanAttack = true;
    }

    public void Attack(AttackBase attackType)
    {
        if (_staminaController.Stamina - attackType.StaminaCost < 0)
            return;
        _currentAttackCombination.Add(attackType);
        _attackTimeBonuse = DefineAttackTimeBonusType();
        _attackMode.TurnOn();
        if (CanAttack)
        {
            CanAttack = false;
            _hitController.CheckForHit(attackType);
            _animatorAttack.PlayAnimationWithDuration(attackType.AnimationName, attackType.Duration + _attackTimeBonuse);
            _playerMove.MoveByDirection(attackType.MovePlayerTime, attackType.MovePlayerSpeed);
            _staminaController.SpendStamina(attackType.StaminaCost);
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

    public void ClearCurrentAttackCombinations()
    {
        _currentAttackCombination.Clear();
    }
}