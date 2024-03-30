using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Move : MonoBehaviour
{
    [SerializeField, Min(0)] private float _speed;
    [SerializeField] private PlayerInputJoystick _playerInputJoystick;

    private float _tresholdInput = 0.01f;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (_playerInputJoystick.MovmentInput.sqrMagnitude > _tresholdInput)
        {
            _rigidbody2D.MovePosition(_rigidbody2D.position + _playerInputJoystick.MovmentInput * _speed * Time.deltaTime);

            _animator.SetFloat(AnimationConstants.Speed, _playerInputJoystick.MovmentInput.magnitude);
        }
    }
}