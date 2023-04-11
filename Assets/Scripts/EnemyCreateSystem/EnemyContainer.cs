using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContainer : MonoBehaviour
{
    [SerializeField] private Enemy _enemyMeleePrefab;
    private PoolMono<Enemy> _enemyMeleePool;
    [SerializeField] private int _enemyMeleePoolcapacity;
    [SerializeField] private bool _enemyMeleePoolautoExpand;
    private static EnemyContainer _instance;
    void Start()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        _enemyMeleePool = new PoolMono<Enemy>(_enemyMeleePrefab, _enemyMeleePoolcapacity, transform);
        _enemyMeleePool.AutoExpand = _enemyMeleePoolautoExpand;
    }

    public void CreateEnemyMelee(Vector2 position)
    {
        var bloodSpot = _enemyMeleePool.GetFreeElement();
        bloodSpot.transform.position = position;
    }
    public static EnemyContainer GetInstance()
    {
        return _instance;
    }
}
