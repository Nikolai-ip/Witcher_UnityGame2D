using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float _dXShake;
    [SerializeField] private float _dYShake;
    [SerializeField] private int _shakeCount;
    [SerializeField] private float _delayBetweenShake;
    private Vector3 originVector;

    private void Start()
    {
        FindObjectOfType<HitController>().onHit += ShakeCamera;
        originVector = transform.position;
    }
    private void ShakeCamera()
    {
        StartCoroutine(Shake());
    }
    private IEnumerator Shake()
    {
        transform.position = new Vector3(transform.position.x - _dXShake, transform.position.y - _dYShake, transform.position.z);
        for (int i = 0; i < _shakeCount; i++)
        {
            transform.position = new Vector3(transform.position.x + _dXShake*2, transform.position.y + _dYShake*2, transform.position.z);
            int sign = (int)Mathf.Pow((-1), i + 1);
            _dXShake *= sign;
            _dYShake *= sign;
            yield return new WaitForSeconds(_delayBetweenShake);
        }
        transform.position = originVector;
    }
}
