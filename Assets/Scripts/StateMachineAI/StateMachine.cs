using StateMachineAI;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [Header("Move")]
    public float speed;

    [Header("Attack state values")]
    public float delayBetweenComboAttacks;
    public float delayBetweenAttack;

    [Header("Attack side")]
    public float damageSideAttack;
    public float delayBeforeSideAttack;
    [Header("Attack Up")]
    public float damageUpAttack;
    public float delayBeforeUpAttack;
    [Space]
    [Header("Take damage stun")]
    public float takeDamageStunDuration;
    [Header("Countre attack stun")]
    public float countreAttackStunDuration;
    [Space]
    [SerializeField] private Transform _hitPoint;
    [SerializeField] private float _flipDelay;
    [Header("Die")]
    public bool bodyIsDisappearence;
    public float delayBeforeBodyDisappearence;
    public float disappearenceDuration;
    public float dieMoveVelocity;
    public float dieMoveDuration;

    public Transform HitPoint => _hitPoint;
    public Collider2D Collider { get; private set; }
    private IdleState _idle;
    public MoveState Move { get; private set; }
    public AttackState Attack { get; private set; }
    public CountreAttackStunState CountreAttackStun { get; private set; }
    public TakeDamageStunState TakeDamageStun { get; private set; }
    public DieState Die { get; private set; }
    private EscapeState _escape;
    public CameraShake CameraShake { get; private set; }
    public Player Player { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public Enemy Instance { get; private set; } 
    public SpriteRenderer SpriteRenderer { get; private set; }
    private State _currentState;
    [Space]
    [Header("Escape")]
    public float escapeMoveDuration;
    public float escapeVelocity;
    public float escapeImortalDuration;
    [SerializeField] private float _distanceToPlayerForEscape;
    [SerializeField] private int _escapeProbability;
    [Space]
    [SerializeField] private float _distanceToAttack;
    [SerializeField] private float _distanceToMove;
    public float DistanceToMove => _distanceToMove;
    public float DistanceToAttack => _distanceToAttack;
    public Animator Animator { get; private set; }
    public bool canFlip = true;
    public bool IsStuned { get; set; } = false;
    public Canvas EntityUIBars { get; private set; }
    private void Awake()
    {
        EntityUIBars = GetComponentInChildren<Canvas>();
    }
    private void Start()
    {
        Collider = GetComponent<Collider2D>();
        Rb = GetComponent<Rigidbody2D>();
        Player = FindObjectOfType<Player>();
        Player.GetComponent<HitController>().onTryHit += OnPlayerTryAttackHandler;
        Animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Instance = GetComponent<Enemy>();   
        _idle = new IdleState(this);
        Move = new MoveState(this);
        Attack = new AttackState(this);
        CountreAttackStun = new CountreAttackStunState(this);
        TakeDamageStun = new TakeDamageStunState(this);
        Die = new DieState(this);
        _escape = new EscapeState(this);
        CameraShake = FindObjectOfType<CameraShake>();
        ChangeState(GetBaseState());
    }

    private void Update()
    {
        try
        {
            TryUpdateLogic();
        }
        catch (Exception e)
        {
            Debug.LogAssertion(e);
        }

    }

    public void ChangeState(State newState)
    {
        if (InstanceCantChangeState(newState))
        {
            return;
        }
        _currentState = newState;
       // Debug.Log(_currentState.GetType().Name);
    }
    private bool InstanceCantChangeState(State newState)
    {
        return newState is TakeDamageStunState && _currentState is CountreAttackStunState || newState is TakeDamageStunState && _currentState is EscapeState;
    }
    private void TryUpdateLogic()
    {
        if (_currentState != null)
        {
            _currentState.UpdateLogic();
            if (Mathf.Sign(_currentState.GetDistanceToPlayerWithSign()) != transform.localScale.x && canFlip)
            {
                canFlip = false;
                StartCoroutine(Flip());
            }
        }
    }

    private IEnumerator Flip()
    {
        yield return new WaitForSeconds(_flipDelay);
        canFlip = true;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
    }

    private void FixedUpdate()
    {
        try
        {
            TryUpdatePhysic();
        }
        catch (Exception e)
        {
            Debug.LogAssertion(e);
        }
    }

    private void TryUpdatePhysic()
    {
        if (_currentState != null)
        {
            _currentState.UpdatePhysics();
        }
    }

    public State GetBaseState()
    {
        return _idle;
    }
    public void RemoveOnTryHitListener()
    {
        Player.GetComponent<HitController>().onTryHit -= OnPlayerTryAttackHandler;
    }
    private async void OnPlayerTryAttackHandler()
    {
        int probability = UnityEngine.Random.Range(0, 100);
        if (probability <= _escapeProbability && _distanceToPlayerForEscape > _currentState.GetDistanceToPlayer() && !Die.IsDying)
        {
            Instance.canTakeDamage = false;
            Animator.SetTrigger("Escape");
            ChangeState(_escape);
            await Task.Delay((int)(escapeImortalDuration * 1000));
            Instance.canTakeDamage = true;
        }
    }
    public void DisableStates()
    {
        _idle = null;
        Move = null;
        Attack = null;
        CountreAttackStun = null;
        TakeDamageStun = null;
        _escape = null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _distanceToAttack);
        Gizmos.DrawWireSphere(_hitPoint.position, _distanceToAttack / 2);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _distanceToMove);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _distanceToPlayerForEscape);
    }
}