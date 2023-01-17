using AISystem;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class StanEnemyController : MonoBehaviour
{
    private State[] _states;
    private AIStateMachine _stateMachine;
    private Animator _animatorController;
    [SerializeField] private int _stanDurationMs;

    void Start()
    {
        _states = GetComponents<State>();
        _stateMachine = GetComponent<AIStateMachine>();
        _animatorController = GetComponent<Animator>();
    }

    public void Stan()
    {
        if (_states != null)
        {
            _animatorController.SetTrigger("Stan");
            StartCoroutine(TurnOffStatesCourutine());
        }
    }
    private IEnumerator TurnOffStatesCourutine()
    {
        float time = 0;
        while (time<_stanDurationMs)
        {
            _stateMachine.enabled = false;
            time += Time.deltaTime; 
            TurnOffStates();
            yield return null;
        }
        _stateMachine.enabled = true;

    }
    private void TurnOffStates()
    {
        foreach (var state in _states)
        {
            state.Disable();
        }
    }
}
