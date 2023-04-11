using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSpot : MonoBehaviour
{
    private SpriteRenderer _sp;
    [SerializeField] Sprite[] _bloodSpotSprites;
    [SerializeField] private float _visibleTime;
    void Start()
    {
        _sp = GetComponent<SpriteRenderer>();
        _sp.sprite = _bloodSpotSprites[Random.Range(0, _bloodSpotSprites.Length-1)];
        _sp.color = new Color(1, 1, 1, Random.Range(0.5f, 1));
        transform.localScale = Vector3.one * Random.Range(0.5f, 1);
        transform.localScale = new Vector3(Mathf.Sign(Random.Range(-1, 1)) * transform.localScale.x, transform.localScale.y);
        StartCoroutine(SmoothHide());
    }
    private IEnumerator SmoothHide()
    {
        float time = 0;
        while (_sp.color.a>0)
        {
            time+= Time.deltaTime;  
            _sp.color = new Color(1, 1, 1, 1 - time / _visibleTime);
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
