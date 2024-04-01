using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private EnemyDetector _enemyDetector;

    [Space(10)]
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackCooldown;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    private Transform _target;

    private Transform _transform;
    private Vector2 _direction;
    private float _distacne;
    private float _tresholdFaceRight = 0.01f;
    private float _tresholdFaceLeft = -0.01f;

    public bool IsAttack { get; private set; } = true;
    public int Damage => _damage;

    public void Init(PlayerController target)
    {
        _target = target.transform;
        _enemyDetector.Init(target);

        _transform = transform;

        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_enemyDetector.PlayerDetected)
        {
            _distacne = Vector2.Distance(_target.transform.position, _transform.position);
            _direction = (_target.transform.position - _transform.position).normalized;
        }
    }

    private void FixedUpdate()
    {
        if (_enemyDetector.PlayerDetected)
        {
            RotationMove(_direction);

            if (_distacne > _attackRange)
                Move();
            else
                Attack();
        }
    }

    private void Move()
    {
        if (_enemyDetector.PlayerDetected)
            _rigidbody2D.MovePosition((Vector2)_transform.position + _direction * _speed * Time.deltaTime);
    }

    private void Attack()
    {
        if (!IsAttack)
            return;

        _animator.SetTrigger(AnimationConstants.Attack);
        StartCoroutine(AttackCoolDown());
    }

    private IEnumerator AttackCoolDown()
    {
        IsAttack = false;
        yield return new WaitForSeconds(_attackCooldown);
        IsAttack = true;
    }

    private void RotationMove(Vector2 direction)
    {
        if (direction.x < _tresholdFaceRight)
            _transform.localScale = Vector3.one;
        else if (direction.x > _tresholdFaceLeft)
            _transform.localScale = new Vector3(-1, 1, 1);
    }

    private void OnEnable() => _health.DiedChanged += OnDiedChanged;
    private void OnDisable() => _health.DiedChanged -= OnDiedChanged;

    public void OnDiedChanged() => Destroy(gameObject);
}