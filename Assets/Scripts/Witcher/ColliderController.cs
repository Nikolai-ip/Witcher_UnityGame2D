using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    private BoxCollider2D _collider;
    [SerializeField] private float shutdownColliderDuration;
    private LayerMask _enemyCollider = 7;
    private LayerMask _playerCollider = 6;

    private void Start()
    {
        _collider= GetComponent<BoxCollider2D>();
       
    }
    public async void TurnOff()
    {
        float time = 0;
        Physics2D.IgnoreLayerCollision(_playerCollider, _enemyCollider, true);
        while (time< shutdownColliderDuration)
        {
            time += Time.deltaTime;
            await Task.Yield();
        }
        Physics2D.IgnoreLayerCollision(_playerCollider, _enemyCollider, false);

    }
}
