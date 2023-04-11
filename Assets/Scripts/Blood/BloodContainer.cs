using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodContainer : MonoBehaviour
{
    [SerializeField] private BloodSpot _bloodSpotPrefab;
    private PoolMono<BloodSpot> _bloodSpotPool;
    [SerializeField] private int _bloodSpotPoolcapacity;
    [SerializeField] private bool _bloodSpotPoolautoExpand;
    private static BloodContainer _instance;
    void Start()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        _bloodSpotPool = new PoolMono<BloodSpot>(_bloodSpotPrefab, _bloodSpotPoolcapacity, transform);
        _bloodSpotPool.AutoExpand = _bloodSpotPoolautoExpand;
    }

    public void CreateBloodSpot(Vector2 position)
    {
        var bloodSpot = _bloodSpotPool.GetFreeElement();
        bloodSpot.transform.position = position;
    }
    public static BloodContainer GetInstance()
    {
        return _instance;
    }
}
