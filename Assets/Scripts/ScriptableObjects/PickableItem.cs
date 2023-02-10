using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickableItem : ScriptableObject
{
    [Header(nameof(PickableItem))]
    [SerializeField] private string _name;
    [SerializeField] private ItemStackBehavior _stackBehavior;
    [SerializeField] private byte _maxStackAmount = 1;

    public string Name => _name;
    public ItemStackBehavior StackBehavior => _stackBehavior;
    public byte MaxStackAmount => _maxStackAmount;

    public virtual bool CanPickup(Inventory inventory)
    {
        return true;
    }

    public virtual void Pickup(Inventory inventory)
    {

    }
}
