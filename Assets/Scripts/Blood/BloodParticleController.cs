using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticleController : MonoBehaviour
{
    [SerializeField] private GameObject[] _bloodParticalPrefab;

    private void Start()
    {
        GetComponent<Enemy>().OnTakeDamage += CreateBloodParticle;
    }
    private void CreateBloodParticle()
    {
        Instantiate(_bloodParticalPrefab[Random.Range(0,_bloodParticalPrefab.Length-1)],transform);
    }
}
