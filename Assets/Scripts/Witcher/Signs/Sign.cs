using System.Threading.Tasks;
using UnityEngine;

public abstract class Sign : MonoBehaviour
{
    protected Animator animator;
    [SerializeField] protected float timeLive;
    [SerializeField] protected float delayExecute;

    protected void Initialize()
    {
        animator = GetComponent<Animator>();
        Disable();
    }

    public virtual async void Execute()
    {
        await Task.Delay((int)(delayExecute * 1000));
        gameObject.SetActive(true);
        animator.SetTrigger("CastSign");
        DisableAfterWhile();
    }

    public virtual void Disable()
    {
        gameObject.SetActive(false);
    }

    protected async void DisableAfterWhile()
    {
        await Task.Delay((int)(timeLive * 1000));
        Disable();
    }
}