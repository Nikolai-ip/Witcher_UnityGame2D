using Assets.Scripts.Witcher.CombatSystem.BlockType;
using System;
using System.Collections;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    private AnimatorController _animatorController;
    private AttackMode _attackMode;
    [SerializeField] private bool _entityOnBlock;
    [SerializeField] private CountreAttackBase _currentCountreAttackType;
    public bool EntityOnBlock => _entityOnBlock;
    [SerializeField] private bool _canCountreAttack = false;
    [SerializeField] private bool _canBlock = true;
    [SerializeField] private float _delayBetweenBlocks;
    [SerializeField] private float _countreAttackWindowDuration;
    [SerializeField] private float _blockSpendStaminaValue;
    private PlayerMove _playerMove;
    private StaminaController _staminaController;
    public event Action onCountreAttack;
    public event Action onBlock;
    private AudioFighterController _audio;
    private void Start()
    {
        _animatorController = GetComponent<AnimatorController>();
        _attackMode = GetComponent<AttackMode>();
        _playerMove = GetComponent<PlayerMove>();
        _staminaController = GetComponent<StaminaController>();
        _audio = GetComponent<AudioFighterController>();
    }

    public void Block(bool entityOnBlock, CountreAttackBase countreAttackType)
    {
        _attackMode.TurnOn();
        _animatorController.PlayBlockAnimation(entityOnBlock, countreAttackType);
        _entityOnBlock = entityOnBlock;
        _playerMove.SetPlayerCanWalk(!entityOnBlock);
        _staminaController.CanRecoveryStamina = !entityOnBlock;
        if (_canBlock)
        {
            _currentCountreAttackType = countreAttackType;
            _canBlock = false;
            _canCountreAttack = true;
            StartCoroutine(TurnOnCanBlock());
            StartCoroutine(TurnOffCanCountreAttack());
        }
        if (!entityOnBlock)
        {
            _currentCountreAttackType = null;
        }
    }

    private IEnumerator TurnOnCanBlock()
    {
        yield return new WaitForSeconds(_delayBetweenBlocks);
        _canBlock = true;
    }

    private IEnumerator TurnOffCanCountreAttack()
    {
        yield return new WaitForSeconds(_countreAttackWindowDuration);
        _canCountreAttack = false;
    }

    public bool EntityBlock(AttackBase attack, GameObject damager)
    {
        if (_currentCountreAttackType == null) return false;
        if (_currentCountreAttackType.HitType != attack.HitType) return false;
        if (_canCountreAttack)
        {
            onCountreAttack?.Invoke();
            _audio.PlayCountreAttackAudioClip();
            StunDamager(damager);
        }
        else
        {
            _staminaController.SpendStamina(_blockSpendStaminaValue);
            onBlock?.Invoke();
            _audio.PlayBlockAttackAudioClip();
            BlockDamager(damager);
        }
        return true;
    }

    private void StunDamager(GameObject damager)
    {
        if (damager.TryGetComponent(out StanEnemyController stanEnemyController))
        {
            stanEnemyController.CountreAttackStun();
        }
    }

    private void BlockDamager(GameObject damager)
    {
        if (damager.TryGetComponent(out StanEnemyController stanEnemyController))
        {
            stanEnemyController.BlockStun();
        }
    }
}