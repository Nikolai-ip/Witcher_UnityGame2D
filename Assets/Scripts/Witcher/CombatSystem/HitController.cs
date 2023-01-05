using Assets.Scripts;
using System;
using System.Collections;
using UnityEngine;

public class HitController : MonoBehaviour
{
    [SerializeField] private Transform _hitPoint;
    [SerializeField] private float _hitRange;
    [SerializeField] private float _hitTimeDelay;
    [SerializeField] private int _hitDamage;
    public event Action onHit;

    private void Start()
    {
        if (_hitPoint == null) Debug.LogWarning("Hit point is null");
    }
    public void CheckForHit()
    {
        StartCoroutine(ExecuteHitAttackDelay());
    }
    private IEnumerator ExecuteHitAttackDelay()
    {
        float time = 0;
        while (time<_hitTimeDelay)
        {
            time += Time.deltaTime;
            yield return null;
        }
        Hit();
    }
    private void Hit()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_hitPoint.position, _hitRange);
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out Damageable damageableEntity))
            {
                onHit?.Invoke();
                damageableEntity.TakeDamage(_hitDamage);
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (_hitPoint == null) return;
        Gizmos.DrawWireSphere(_hitPoint.position, _hitRange);
    }
}
