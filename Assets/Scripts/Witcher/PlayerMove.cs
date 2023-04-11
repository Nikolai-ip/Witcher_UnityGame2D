using System;
using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _minMoveX;
    [SerializeField] private float _escapeTime;
    [SerializeField] private float _escapeSpeed;
    [SerializeField] private float _escapeStaminaCost;
    [SerializeField] private float _escapeDelay;
    private ColliderController _collider;
    private AttackMode _attackMode;
    private AnimatorController _animatorController;
    private Player _player;
    private bool _playerCanWalk = true;
    public bool PlayerCanEscape { get; set; } = true;
    private StaminaController _staminaController;
    private AudioFighterController _audio;
    private void Start()
    {
        _player = GetComponent<Player>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animatorController = GetComponent<AnimatorController>();
        _attackMode = GetComponent<AttackMode>();
        _collider = GetComponent<ColliderController>();
        _staminaController = GetComponent<StaminaController>();
        _audio = GetComponent<AudioFighterController>();
    }

    public void Move(float moveX)
    {
        if (Mathf.Abs(moveX) > _minMoveX && _playerCanWalk)
        {
            _animatorController.PlayWalkAnimation();
            _rigidbody2D.velocity = new Vector2(moveX * _moveSpeed, 0);
            FlipCheck(moveX);
        }
        else if (_playerCanWalk)
        {
            StopWalk();
        }
    }

    private void StopWalk()
    {
        _animatorController.StopWalkAnimation();
        _rigidbody2D.velocity = Vector2.zero;
    }

    private void FlipCheck(float moveX)
    {
        transform.localScale = new Vector2(Mathf.Sign(moveX), 1);
    }

    public void Escape()
    {
        if ((_staminaController.Stamina - _escapeStaminaCost < 0) || !PlayerCanEscape)
            return;
        PlayerCanEscape = false;
        _player.CanTakeDamage = false;
        _playerCanWalk = false;
        _staminaController.SpendStamina(_escapeStaminaCost);
        _attackMode.TurnOn();
        _animatorController.PlayEscapeAnimation();
        _collider.IgnorePlayerLayerWithEnemyCollider();
        StopCoroutine(MoveCorutine(_escapeTime, _escapeSpeed));
        StartCoroutine(MoveCorutine(_escapeTime, _escapeSpeed));
        StartCoroutine(TurnOnPlayerCanEscape());
        _audio.PlayEscapeAudioClip();
    }

    private IEnumerator MoveCorutine(float moveTime, float moveSpeed)
    {
        float moveX = transform.localScale.x * moveSpeed;
        _rigidbody2D.velocity = new Vector2(moveX, 0);
        _playerCanWalk = false;
        yield return new WaitForSeconds(moveTime);
        _playerCanWalk = true;
        _player.CanTakeDamage = true;
        _playerCanWalk = true;
    }

    public void MoveByDirection(float moveTime, float moveSpeed)
    {
        StopCoroutine(MoveCorutine(moveTime, moveSpeed));
        StartCoroutine(MoveCorutine(moveTime, moveSpeed));
    }

    private IEnumerator TurnOnPlayerCanEscape()
    {
        yield return new WaitForSeconds(_escapeDelay);
        PlayerCanEscape = true;
    }

    public void SetPlayerCanWalk(bool playerCanWalk)
    {
        _playerCanWalk = playerCanWalk;
    }
}