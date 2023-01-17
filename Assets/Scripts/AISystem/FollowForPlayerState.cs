using System.Collections;
using UnityEngine;

public class FollowForPlayerState : State
{
    private Rigidbody2D _rb;
    [SerializeField] private float _speed;
    private Rigidbody2D _target;
    [SerializeField] private Animator _animatorController;
    public override void Disable()
    {
        base.Disable();
        _animatorController.SetBool("IsWalk", false);
    }
    public override void Enable()
    {
        base.Enable();
        _animatorController.SetBool("IsWalk", true);
    }
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animatorController = GetComponent<Animator>();
        _target = FindObjectOfType<Player>().GetComponent<Rigidbody2D>();
        Disable();
    }

    private void Update()
    {
        if (_rb != null)
        {
            Vector2 newPosition = Vector2.MoveTowards(_rb.position, _target.position, _speed * Time.deltaTime);
            _rb.position = newPosition;
        }
    }
}