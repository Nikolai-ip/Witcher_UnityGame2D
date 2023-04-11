using UnityEngine;

public class EntityFollower : MonoBehaviour
{
    [SerializeField] private Entity _target;
    public Entity Entity => _target;
    [SerializeField] private float _dx;
    [SerializeField] private float _dy;

    private void Awake()
    {
        if (_target == null)
            _target = GetComponentInParent<Entity>();
        this.transform.position = new Vector3()
        {
            x = _target.transform.position.x - _dx,
            y = _target.transform.position.y - _dy,
            z = this.transform.position.z,
        };
        transform.SetParent(null);
    }

    private void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
        }
        Vector3 target = new Vector3()
        {
            x = _target.transform.position.x - _dx,
            y = _target.transform.position.y - _dy,
            z = this.transform.position.z,
        };

        this.transform.position = target;


    }
}