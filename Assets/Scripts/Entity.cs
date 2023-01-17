using Assets.Scripts;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected int health;
    protected Animator animator;
    protected string dieNameAnimation = "Die";
    protected string takeDamageNameAnimation = "TakeDamage";
    [SerializeField] protected float timeForDie = 3;
    protected new ColliderController collider;
    [SerializeField] protected bool isLive = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<ColliderController>();
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
}