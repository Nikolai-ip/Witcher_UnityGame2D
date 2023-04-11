using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private Enemy _enemyMeleePrefab;
    [SerializeField] private float _distanceToCreate;
    private int _clock = 0;
    [SerializeField] private float _frequencyCreateEnemy;
    private void Start()
    {
        CreateEnemyMelee();
        StartCoroutine(CreateEnemyesCourutine());
    }
    private IEnumerator CreateEnemyesCourutine()
    {
        while (true)
        {
            CreateEnemyMelee();
            yield return new WaitForSeconds(_frequencyCreateEnemy);
        }
    }
    private void CreateEnemyMelee()
    {
        _clock++;
        Instantiate(_enemyMeleePrefab, new Vector2(Mathf.Pow(-1, _clock) * _distanceToCreate, 0), new Quaternion());
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(transform.position.x,0),new Vector3(_distanceToCreate,0));
        Gizmos.DrawLine(new Vector3(transform.position.x, 0), new Vector3(-_distanceToCreate, 0));

    }
}
