using System.Collections.Generic;
using UnityEngine;


public class BloodSpotSpawner : MonoBehaviour
{
    private ParticleSystem _part;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    [SerializeField] private float _spawnBloodSpotProbability;


    void Start()
    {
        _part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();

    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = _part.GetCollisionEvents(other, collisionEvents);
        int i = 0;
        while (i < numCollisionEvents)
        {
            int probability = Random.Range(0, 100);
            if (probability <= _spawnBloodSpotProbability)
            {
                Vector3 pos = collisionEvents[i].intersection;
                BloodContainer.GetInstance().CreateBloodSpot(pos);
            }
            i++;
        }
    }

}
