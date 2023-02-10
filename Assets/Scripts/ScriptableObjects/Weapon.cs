using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Pikable Items/Weapon", order = 51)]
public class Weapon : PickableItem
{
    [Header(nameof(Weapon))]
    [SerializeField] private float _damage = 1;
    [SerializeField] private float _interval = 0.2f;

    public float Damage => _damage;
    public float Interval => _interval;
}
