using UnityEngine;

public class PlayerInputJoystick : MonoBehaviour
{
    [SerializeField] private FixedJoystick _moveInputJoystick;

    public Vector2 MovmentInput { get; private set; }

    private void Update()
    {
        MovmentInput = _moveInputJoystick.Direction;
    }
}