using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class EscapeState : State
{
    private Rigidbody2D _rb;
    [SerializeField] private float _escapeTime;
    [SerializeField] private float _escapeSpeed;
    private bool _isCanEscape = true;
    private Animator _animator;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        Disable();
    }
    private void Update()
    {
        if (_isCanEscape)
        {
            StopAllCoroutines();
            _isCanEscape = false;
            StartCoroutine(Escape());
        }
    }
    private IEnumerator Escape()
    {

        _animator.SetTrigger("Escape");
        float time = 0;
        float moveX = -transform.localScale.x * _escapeSpeed;
        _rb.velocity = new Vector2(moveX, 0);
        while (time < _escapeTime)
        {
            time += Time.deltaTime;
            yield return null;
        }
        _rb.velocity = Vector2.zero;
        _isCanEscape = true;
        Disable();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(new Vector2(transform.position.x- (_escapeTime*_escapeSpeed), transform.position.y), 0.2f);
    }
}
