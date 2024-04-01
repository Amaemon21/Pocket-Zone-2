using Inventory;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private EntryPoint _entryPoint;
    [SerializeField, Min(0)] private float _speed;
    [SerializeField] private PlayerInputJoystick _playerInputJoystick;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    private float _tresholdInput = 0.01f;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Item>(out Item item))
        {
            _entryPoint.InventoryService.AddItemsToInventory(_entryPoint.CachedOwnerId, item.ItemId, item.Sprite, item.Amount);
            Destroy(item.gameObject);
        }
    }
}