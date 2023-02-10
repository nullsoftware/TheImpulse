using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PickableItemInfo
{
    public PickableItem Item { get; }
    public byte Amount { get; set; }

    public PickableItemInfo(PickableItem item, byte initAmount = 1)
    {
        Item = item;
        Amount = initAmount;
    }

    public static implicit operator PickableItem(PickableItemInfo itemInfo) => itemInfo.Item;
}