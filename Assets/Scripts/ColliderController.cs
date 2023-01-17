using System.Threading.Tasks;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    [SerializeField] private float shutdownColliderDuration;
    private int _playerLayerMask;
    private int _enemyLayerMask;

    private void Start()
    {
        _playerLayerMask = (int)Mathf.Log(LayerMask.GetMask("Player"), 2);
        _enemyLayerMask = (int)Mathf.Log(LayerMask.GetMask("Enemy"), 2);
    }

    public async void IgnorePlayerLayerWithEnemyCollider()
    {
        float time = 0;
        Physics2D.IgnoreLayerCollision(_playerLayerMask, _enemyLayerMask, true);
        while (time < shutdownColliderDuration)
        {
            time += Time.deltaTime;
            await Task.Yield();
        }
        Physics2D.IgnoreLayerCollision(_playerLayerMask, _enemyLayerMask, false);
    }

    public async void IgnorePlayerLayerWithEnemyCollider(float shutdownDuration)
    {
        float time = 0;
        Physics2D.IgnoreLayerCollision(_playerLayerMask, _enemyLayerMask, true);
        while (time < shutdownDuration)
        {
            time += Time.deltaTime;
            await Task.Yield();
        }
        Physics2D.IgnoreLayerCollision(_playerLayerMask, _enemyLayerMask, false);
    }
}