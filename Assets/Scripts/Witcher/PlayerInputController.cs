using Assets.Scripts.Witcher.CombatSystem.BlockType;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : InputController
{
    private PlayerInput _playerInput;

    public event Action OnIteractButtonPerformed;

    private BlockController _blockController;
    public InputAction blockAction;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Main.LeftAttack.performed += context => OnLeftAttackButtonClick();
        _playerInput.Main.RightAttack.performed += context => OnRightAttackButtonClick();
        _playerInput.Main.PirouetteAttack.performed += context => OnPirouetteAttackButtonClick();
        _playerInput.Main.Escape.performed += context => OnEscapeButtonClick();
        _playerInput.Main.Iteract.performed += context => OnIteractButtonClick();
        _playerInput.Main.CountreAttackUp.performed += context => OnBlockButtonClick(true, new CountreAttackUp());
        _playerInput.Main.CountreAttackUp.canceled += context => OnBlockButtonClick(false, new CountreAttackUp());
        _playerInput.Main.CountreAttackSide.performed += context => OnBlockButtonClick(true, new CountreAttackSide());
        _playerInput.Main.CountreAttackSide.canceled += context => OnBlockButtonClick(false, new CountreAttackSide());
    }

    private void OnIteractButtonClick()
    {
        OnIteractButtonPerformed?.Invoke();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        playerAttack = GetComponent<AttackController>();
        signCaster = GetComponentInChildren<SignCaster>();
        _blockController = GetComponent<BlockController>();
    }

    private void OnEscapeButtonClick()
    {
        playerMove.Escape();
    }

    private void OnLeftAttackButtonClick()
    {
        playerAttack.Attack(leftAttack);
    }

    private void OnRightAttackButtonClick()
    {
        playerAttack.Attack(rightAttack);
    }

    private void OnPirouetteAttackButtonClick()
    {
        playerAttack.Attack(pirouetteAttack);
    }

    private void OnMove(Vector2 move)
    {
        playerMove.Move(move.x);
    }

    private void FixedUpdate()
    {
        OnMove(_playerInput.Main.Move.ReadValue<Vector2>());
    }

    private void OnBlockButtonClick(bool buttonIsHoldOn, CountreAttackBase countreAttackType)
    {
        _blockController.Block(buttonIsHoldOn, countreAttackType);
    }
}