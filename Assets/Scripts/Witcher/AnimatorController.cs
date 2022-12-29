using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator _animator;

    private readonly string _walkAnimationName = "IsWalk";


    private void Start()
    {
        _animator = GetComponent<Animator>();  
    }

    public void PlayIdleAnimation()
    {
        _animator.Play("Idle");
    }

    public void PlayWalkAnimation()
    {
        _animator.SetBool(_walkAnimationName, true);
    }

    public void StopWalkAnimation()
    {
        _animator.SetBool(_walkAnimationName, false);
    }

}