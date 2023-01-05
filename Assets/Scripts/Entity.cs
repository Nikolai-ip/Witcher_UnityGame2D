using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected int _health;
    protected void Die()
    {
        Destroy(gameObject); 
    }
}
