using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovable : MonoBehaviour
{
    [SerializeField, Min(0)] private float _speed;

    [SerializeField] private FixedJoystick _moveInputJoystick;

    private Vector2 _movmentInput;

    private float _tresholdInput = 0.01f;

    private float _tresholdFaceRight = 0.01f;
    private float _tresholdFaceLeft = -0.01f;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _movmentInput = _moveInputJoystick.Direction;
    }

    private void FixedUpdate()
    {
        if (_movmentInput.sqrMagnitude > _tresholdInput)
        {
            _rigidbody2D.MovePosition(_rigidbody2D.position + _movmentInput * _speed * Time.deltaTime);

            _animator.SetFloat(AnimationConstants.Speed, _movmentInput.sqrMagnitude);

            _animator.SetFloat(AnimationConstants.Horizontal, _movmentInput.x);
            _animator.SetFloat(AnimationConstants.Vertical, _movmentInput.y);

            RotationMove();
        }
    }

    private void RotationMove()
    {
        if (_movmentInput.x > _tresholdFaceRight)
            transform.localScale = Vector3.one;
        else if (_movmentInput.x < _tresholdFaceLeft)
            transform.localScale = new Vector3(-1, 1, 1);
    }
}