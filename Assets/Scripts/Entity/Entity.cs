using System;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField]
    protected float health;

    [SerializeField]
    public float Health
    {
        get => health; protected set
        {
            health = value;
            onHealthchanged?.Invoke(health);
        }
    }

    public float MaxHealth { get; protected set; }
    [SerializeField] protected float maxStamina;
    [SerializeField] protected float minStamina;
    protected Animator animator;
    protected string dieNameAnimation = "Die";
    protected string takeDamageNameAnimation = "TakeDamage";
    [SerializeField] protected float timeForDie = 3;
    protected new ColliderController collider;
    [SerializeField] protected bool isLive = true;
    protected Rigidbody2D rb;
    public float MaxStamina => maxStamina;
    public float MinStamina => minStamina;

    public event Action<float> onHealthchanged;
    public event Action onDestroy;
    private void Start()
    {
        BaseInit();
    }
    protected virtual void BaseInit()
    {
        MaxHealth = Health;
        animator = GetComponent<Animator>();
        collider = GetComponent<ColliderController>();
        rb = GetComponent<Rigidbody2D>();
    }
    protected virtual void Die()
    {
        if (isLive)
        {
            enabled = false;
            isLive = false;
            animator.SetTrigger(dieNameAnimation);
            DieDuration();
        }
    }

    protected async void DieDuration()
    {
        float time = 0;
        collider.IgnorePlayerLayerWithEnemyCollider(timeForDie);
        while (time < timeForDie)
        {
            time += Time.deltaTime;
            await Task.Yield();
        }
        if (!gameObject.IsDestroyed()) Destroy(gameObject);
    }
    public virtual void DestroyObject(float delay = 0)
    {
        onDestroy?.Invoke();
        Destroy(gameObject, delay);
    }
}