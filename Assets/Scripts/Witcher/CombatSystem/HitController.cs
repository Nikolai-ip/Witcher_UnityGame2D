using Assets.Scripts;
using System;
using System.Collections;
using UnityEngine;

public class HitController : MonoBehaviour
{
    [SerializeField] private Transform _hitPoint;
    [SerializeField] private float _hitRange;
    [SerializeField] private float _hitTimeDelay;
    [SerializeField] private float _hitDamage;

    public event Action onHit;

    public event Action onMissHit;
    public event Action onTryHit;
    private AudioFighterController _audio;
    private void Start()
    {
        if (_hitPoint == null) Debug.LogWarning("Hit point is null");
        _audio = GetComponent<AudioFighterController>();
    }

    public void CheckForHit(AttackBase attackType)
    {
        StartCoroutine(ExecuteHitAttackDelay(attackType));
    }

    private IEnumerator ExecuteHitAttackDelay(AttackBase attackType)
    {
        float time = 0;
        while (time < _hitTimeDelay)
        {
            time += Time.deltaTime;
            yield return null;
        }
        Hit(attackType);
    }

    private void Hit(AttackBase attackType)
    {
        onTryHit?.Invoke();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_hitPoint.position, _hitRange, LayerMask.GetMask("Enemy"));
        if (colliders.Length == 0)
        {
            onMissHit?.Invoke();
            _audio.PlayTrySwordHitAudioClip();
        }
            
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out Damageable damageableEntity))
            {
                if (collider.GetComponent<Enemy>().canTakeDamage == true)
                {
                    onHit?.Invoke();
                    _audio.PlaySwordHitInBodyAudioClip();

                }
                else
                {
                    onMissHit?.Invoke();
                    _audio.PlayTrySwordHitAudioClip();
                }
                damageableEntity.TakeDamage(_hitDamage, attackType, gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_hitPoint == null) return;
        Gizmos.DrawWireSphere(_hitPoint.position, _hitRange);
    }
}