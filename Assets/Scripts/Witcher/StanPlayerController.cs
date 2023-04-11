using System.Threading.Tasks;
using UnityEngine;

public class StanPlayerController : MonoBehaviour
{
    private InputController _inputController;
    private AnimatorController _animatorController;
    [SerializeField] private int _stanDurationMs;

    private void Start()
    {
        _inputController = GetComponent<InputController>();
        _animatorController = GetComponent<AnimatorController>();
    }

    public async void Stan()
    {
        if (_inputController != null)
        {
            _inputController.enabled = false;
            _animatorController.PlayStanAnimation();
            await Task.Delay(_stanDurationMs);
            _inputController.enabled = true;
        }
    }
}