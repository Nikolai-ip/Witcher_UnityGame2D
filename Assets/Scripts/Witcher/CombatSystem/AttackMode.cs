using System.Threading.Tasks;
using UnityEngine;

public class AttackMode : MonoBehaviour
{
    private bool _atttackModeIsOver;
    private float time;
    private AnimatorController _animator;
    [SerializeField] private float _attackModeDuration;

    private void Start()
    {
        _animator = GetComponent<AnimatorController>();
        _atttackModeIsOver = true;
    }

    public void TurnOn()
    {
        time = 0;
        if (_atttackModeIsOver)
        {
            _atttackModeIsOver = false;
            AttackModeDuration();
        }
    }

    private async void AttackModeDuration()
    {
        _animator.SetWitcherFightAnimator();
        while (time < _attackModeDuration)
        {
            time += Time.deltaTime;
            await Task.Yield();
        }
        _atttackModeIsOver = true;
        _animator.SetWitcherCalmAnimator();
    }
}