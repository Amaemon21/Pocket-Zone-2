using UnityEngine;

public class HealthBar : Bar
{
    [SerializeField] private Health Health;

    private void Awake()
    {
        Slider.maxValue = Health.MaxHealth;
        Slider.value = Slider.maxValue;
    }

    private void OnEnable()
    {
        Health.OnHealthChange += OnValueChanged;
    }

    private void OnDisable()
    {
        Health.OnHealthChange -= OnValueChanged;
    }
}