using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _minMoveX;
    [SerializeField] private float _escapeTime;
    [SerializeField] private float _escapeSpeed;
    private ColliderController _collider;
    private AttackMode _attackMode;
    private AnimatorController _animatorController;
    private Player _it;
    private bool _playerCanWalk = true;

    private void Start()
    {
        _it = GetComponent<Player>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animatorController = GetComponent<AnimatorController>();
        _attackMode = GetComponent<AttackMode>();
        _collider = GetComponent<ColliderController>();
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
        _it.CanTakeDamage = false;
        _attackMode.TurnOn();
        _animatorController.PlayEscapeAnimation();
        _collider.IgnorePlayerLayerWithEnemyCollider();
        StopAllCoroutines();
        StartCoroutine(MoveCorutine(_escapeTime, _escapeSpeed));
        Invoke("EnableCanTakeDAmage", _escapeTime);
    }
    private void EnableCanTakeDAmage()
    {
        _it.CanTakeDamage = true;
    }

    public void MoveByDirection(float moveTime, float moveSpeed)
    {
        StopAllCoroutines();
        StartCoroutine(MoveCorutine(moveTime, moveSpeed));
    }

    private IEnumerator MoveCorutine(float moveTime, float moveSpeed)
    {
        float time = 0;
        float moveX = transform.localScale.x * moveSpeed;
        _rigidbody2D.velocity = new Vector2(moveX, 0);
        _playerCanWalk = false;
        while (time < moveTime)
        {
            time += Time.deltaTime;
            yield return null;
        }
        _playerCanWalk = true;
        StopWalk();
    }
}