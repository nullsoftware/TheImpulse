using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PickableItemContainer : MonoBehaviour
{
    [SerializeField] private PickableItem _item;
    [SerializeField] private byte _amount = 1;

    public byte Amount => _amount;
    public PickableItem Item => _item;
}
