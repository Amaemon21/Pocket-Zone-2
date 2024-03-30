using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private RotationDirection _direction;

    [Space(10)]
    [SerializeField, Min(0)] private float _force = 10f;
    [SerializeField] private int _damage = 10;

    [Space(10)]
    [SerializeField] private Bullet _bullet;

    [Space(10)]
    [SerializeField] private Transform _firePoint;

    [Space(10)]
    [SerializeField] private Animator _animator;

    [Space(5)]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;

    private InputSystem _inputSystem;

    private void Awake()
    {   
        _inputSystem = new InputSystem();

        _inputSystem.Land.Shoot.started += OnShoot;
    }   

    private void OnShoot(InputAction.CallbackContext context) => Shoot();

    private void Shoot()
    {
        BulletInit();

        _animator.SetTrigger(AnimationConstants.Shoot);

        _audioSource.PlayOneShot(_audioClip);
    }

    private void BulletInit()
    {
        Bullet bullet = Instantiate(_bullet, _firePoint.position, _firePoint.rotation);
        bullet.Init(_damage);
        bullet.Rigidbody2D.velocity = _direction.Direction * _force;
    }

    private void OnEnable() => _inputSystem.Enable();
    private void OnDisable() => _inputSystem.Disable();
}