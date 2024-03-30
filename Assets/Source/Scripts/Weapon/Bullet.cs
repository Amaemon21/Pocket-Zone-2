using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }

    private int _damage;

    public void Init(int Damage)
    {
        _damage = Damage;

        Destroy(gameObject, 3);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}