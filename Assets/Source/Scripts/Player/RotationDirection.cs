using UnityEngine;

public class RotationDirection : MonoBehaviour
{
    [SerializeField] private PlayerInputJoystick _playerInputJoystick;

    private Transform _transform;

    private float _tresholdFaceRight = 0.01f;
    private float _tresholdFaceLeft = -0.01f;

    public Vector2 Direction { get; private set; }

    private void Awake()
    {
        _transform = transform;

        Direction = _transform.right;
    }

    private void Update()
    {
        RotationMove(_playerInputJoystick.MovmentInput);
    }

    private void RotationMove(Vector2 direction)
    {
        if (direction.x > _tresholdFaceRight)
        {
            _transform.localScale = Vector3.one;
            Direction = _transform.right;
        }
        else if (direction.x < _tresholdFaceLeft)
        {
            _transform.localScale = new Vector3(-1, 1, 1);
            Direction = -_transform.right;
        }
    }
}