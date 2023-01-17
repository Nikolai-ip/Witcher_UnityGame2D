using AISystem;
using Assets.Scripts;
using System;
using System.Collections;
using UnityEngine;

public class AttackState : State
{
    [SerializeField] private Transform _hitPoint;
    [SerializeField] private float _hitRange;
    [SerializeField] private float _hitTimeDelay;
    [SerializeField] private int _hitDamage;
    [SerializeField] private bool _attackRightNow = false;
    private BoxCollider2D _selfCollider;
    private Animator _animatorController;


    public event Action onHit;

    public override void Disable()
    {
        base.Disable();
        StopAllCoroutines();
        _attackRightNow = false;
    }

    public override void Enable()
    {
        base.Enable();
        if (!_attackRightNow)
        {
            _attackRightNow = true;
            StartCoroutine(ExecuteHitAttackDelay());
        }
    }

    private void Start()
    {
        _animatorController = GetComponent<Animator>();
        _selfCollider = GetComponent<BoxCollider2D>();
        Disable();
    }

    private IEnumerator ExecuteHitAttackDelay()
    {
        var delay = new WaitForSeconds(_hitTimeDelay);
        while (true)
        {
            Hit();
            yield return delay;
        }
    }

    private void Hit()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_hitPoint.position, _hitRange);
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out Player player) && collider != _selfCollider)
            {
                _animatorController.SetTrigger("Attack");
                onHit?.Invoke();
                player.GetComponent<Damageable>().TakeDamage(_hitDamage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_hitPoint == null) return;
        Gizmos.DrawWireSphere(_hitPoint.position, _hitRange);
    }
}