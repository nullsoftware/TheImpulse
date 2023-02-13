using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletTarget : MonoBehaviour
{
    [SerializeField] private float _healthPoints = 100;
    [SerializeField] private UnityEvent _onDamagedEvent = new UnityEvent();
    [SerializeField] private UnityEvent _onDeathEvent = new UnityEvent();

    private float _maxHealth;

    public event UnityAction OnDamaged
    {
        add => _onDamagedEvent?.AddListener(value);
        remove => _onDamagedEvent?.RemoveListener(value);
    }

    public event UnityAction OnDeath
    {
        add => _onDeathEvent?.AddListener(value);
        remove => _onDeathEvent?.RemoveListener(value);
    }

    public float HealthPoints => _healthPoints;
    public float MaxHealth => _maxHealth;

    public bool IsDead => _healthPoints == 0;

    private void Start()
    {
        _maxHealth = _healthPoints;
    }

    public void DealDamage(float damage)
    {
        if (IsDead)
        {
            return;
        }

        damage = Mathf.Max(0, damage);

        _healthPoints -= Mathf.Min(damage, _healthPoints);

        if (_healthPoints == 0)
        {
            _onDeathEvent?.Invoke();
        }
        else
        {
            _onDamagedEvent.Invoke();
        }
    }

    public void Heal(float heal)
    {
        heal = Mathf.Max(0, heal);

        _healthPoints = Mathf.Min(_healthPoints + heal, _maxHealth);
    }
}
