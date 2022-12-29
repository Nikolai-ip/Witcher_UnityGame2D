using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private AnimatorAttackController _animatorController;
    [SerializeField] private float _attackTimeBonuse;
    [SerializeField] private float _succsessTimeBetweenAttack;
    [SerializeField] private float _failedTimeBetweenAttack;
    [SerializeField] private float _attackModeDuration;
    private PlayerMove _playerMove;
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
        _animatorController = GetComponent<AnimatorAttackController>();
        CanAttack = true;
    }

    public void Attack(Attack attackType)
    {
        _currentAttackCombination.Add(attackType);
        _attackTimeBonuse = DefineAttackTimeBonusType();
        if (CanAttack)
        {
            CanAttack = false;  
            _animatorController.PlayAnimationWithDuration(attackType.AnimationName,attackType.AnimationDuration + _attackTimeBonuse);
            _playerMove.MoveByDirection(attackType.MovePlayerTime, attackType.MovePlayerSpeed);
        }
    }

    private async void AttackModeDuration()
    {
        float time = 0;
        _animatorController.SetFightModeAnimation(true);
        while (time < _attackModeDuration)
        {
            time += Time.deltaTime;
            await Task.Yield();
        }
        _animatorController.SetFightModeAnimation(false);

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