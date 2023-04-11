using Assets.Scripts.Witcher.CombatSystem.BlockType;
using System.Collections;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private RuntimeAnimatorController _withcerCalmAnimator;
    [SerializeField] private RuntimeAnimatorController _withcerFightAnimator;

    private readonly string _walkAnimationName = "IsWalk";
    private readonly string _escapeAnimationName = "Escape";
    private readonly string _castAnimationName = "Cast";

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetWitcherFightAnimator()
    {
        _animator.runtimeAnimatorController = _withcerFightAnimator;
    }

    public void SetWitcherCalmAnimator()
    {
        _animator.runtimeAnimatorController = _withcerCalmAnimator;
    }

    public void PlayWalkAnimation()
    {
        _animator.SetBool(_walkAnimationName, true);
    }

    public void StopWalkAnimation()
    {
        _animator.SetBool(_walkAnimationName, false);
    }

    public void PlayEscapeAnimation()
    {
        _animator.SetTrigger(_escapeAnimationName);
    }

    public void PlayStanAnimation()
    {
    }

    public void PlayCastAnimation()
    {
        _animator.SetTrigger(_castAnimationName);
    }

    public void PlayBlockAnimation(bool playerOnBlock, CountreAttackBase countreAttackType)
    {
        if (playerOnBlock)
        {
            _animator.SetTrigger(countreAttackType.AnimationName);
            StartCoroutine(PlayBlockModeAnimation(countreAttackType));
        }
        else
        {
            StopAllCoroutines();
            _animator.SetBool(countreAttackType.AnimationName + "Mode", false);
        }
    }

    private IEnumerator PlayBlockModeAnimation(CountreAttackBase countreAttackType)
    {
        yield return new WaitForSeconds(countreAttackType.AnimationExecuteDuration);
        _animator.SetBool(countreAttackType.AnimationName + "Mode", true);
    }
}