using System.Collections;
using UnityEngine;

public class AttackMode : MonoBehaviour
{
    private AnimatorController _animator;
    [SerializeField] private float _attackModeDuration;
    private BlockController _blockController;
    private AttackController _attackController;

    private void Start()
    {
        _animator = GetComponent<AnimatorController>();
        _blockController = GetComponent<BlockController>();
        _attackController = GetComponent<AttackController>();
    }

    public void TurnOn()
    {
        StopAllCoroutines();
        StartCoroutine(AttackModeDuration());
    }

    private IEnumerator AttackModeDuration()
    {
        _animator.SetWitcherFightAnimator();
        yield return new WaitForSeconds(_attackModeDuration);
        if (!_blockController.EntityOnBlock)
        {
            _animator.SetWitcherCalmAnimator();
            _attackController.ClearCurrentAttackCombinations();
        }
    }
}