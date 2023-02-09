using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletTarget : MonoBehaviour
{
    [SerializeField] private float _healthPoints = 100;

    private float _maxHealth;

    public UnityAction OnDeath;

    public float HealthPoints => _healthPoints;
    public float MaxHealth => _maxHealth;

    private void Start()
    {
        _maxHealth = _healthPoints;
    }

    public void DealDamage(float damage)
    {
        damage = Mathf.Max(0, damage);

        _healthPoints -= Mathf.Min(damage, _healthPoints);

        if (_healthPoints == 0)
        {
            OnDeath?.Invoke();
        }
    }

    public void Heal(float heal)
    {
        heal = Mathf.Max(0, heal);

        _healthPoints = Mathf.Min(_healthPoints + heal, _maxHealth);
    }
}
