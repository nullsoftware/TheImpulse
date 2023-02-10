using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ammo", menuName = "Pikable Items/Ammo", order = 51)]
public class Ammo : PickableItem
{
    [Header(nameof(Ammo))]
    [SerializeField] private Weapon _weapon;

    public Weapon Weapon => _weapon;
}
