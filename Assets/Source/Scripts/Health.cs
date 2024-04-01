using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;

    private float _minValue = 0;

    public event Action<float> HealthChanged;
    public event Action DiedChanged;

    public float MaxHealth => _maxHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (damage >= _minValue)
        {
            if (_currentHealth >= _minValue)
                _currentHealth -= damage;
            
            if (_currentHealth <= _minValue)
                DiedChanged?.Invoke();

            HealthChanged?.Invoke(_currentHealth);
        }
    }
}