using UnityEngine;

public abstract class State : MonoBehaviour
{
    public virtual void Enable() => enabled = true;

    public virtual void Disable() => enabled = false;
}