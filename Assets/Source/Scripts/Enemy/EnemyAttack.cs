using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Enemy Enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(Enemy.Damage);
        }
    }
}