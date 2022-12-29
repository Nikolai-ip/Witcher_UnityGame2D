using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorAttackController : MonoBehaviour
{
    private Animator _animator;
    private readonly string _attackModeAnimationName = "AttackMode";
    private AttackController _playerAttack;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerAttack = GetComponent<AttackController>();
    }
    public void SetFightModeAnimation(bool isAttackMode)
    {
        _animator.SetBool(_attackModeAnimationName, isAttackMode);
    }

    public void PlayAnimationWithDuration(string animationName, float duration)
    {
        _animator.SetTrigger(animationName);
        StartCoroutine(AnimationDuration(duration));
    }
    private IEnumerator AnimationDuration(float duration)
    {
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        _playerAttack.CanAttack = true;
    }
}
