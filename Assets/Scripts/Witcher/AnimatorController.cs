using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private RuntimeAnimatorController _withcerCalmAnimator;
    [SerializeField] private RuntimeAnimatorController _withcerFightAnimator;

    private readonly string _walkAnimationName = "IsWalk";
    private readonly string _escapeAnimationName = "Escape";


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

}