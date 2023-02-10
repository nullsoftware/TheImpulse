using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour, IEnumerable<PickableItemInfo>
{
    [SerializeField] private byte _width = 20;
    [SerializeField] private byte _height = 6;
    [SerializeField] private UnityEvent<PickableItemInfo> _itemPickupEvent = new UnityEvent<PickableItemInfo>();

    private PickableItemInfo[,] _items;

    public event UnityAction<PickableItemInfo> ItemPickup
    {
        remove => _itemPickupEvent.RemoveListener(value);
        add => _itemPickupEvent.AddListener(value);
    }

    public bool IsEmpty => this.ToArray().All(t => t == null);
    public bool IsFull => this.ToArray().All(t => t != null);

    private void Start()
    {
        _items = new PickableItemInfo[_height, _width];
    }

    public void TryCollectItem(PickableItemContainer container)
    {
        if (container.Item.CanPickup(this))
        {
            PickableItemInfo itemInfo;

            switch (container.Item.StackBehavior)
            {
                case ItemStackBehavior.Stackable:
                    itemInfo = FindStackableItem(container);
                    if (itemInfo == null && TryAllocateItem(container.Item, out PickableItemInfo tmp))
                        itemInfo = tmp;

                    if (itemInfo == null)
                        return;

                    itemInfo.Amount += container.Amount;
                    break;

                case ItemStackBehavior.NotStackable:
                case ItemStackBehavior.Unique:
                    TryAllocateItem(container.Item, out itemInfo);

                    if (itemInfo == null)
                        return;

                    break;

                default:
                    throw new NotSupportedException();
            }

            container.Item.Pickup(this);
            container.gameObject.SetActive(false);
            Destroy(container.Item);

            _itemPickupEvent?.Invoke(itemInfo);
        }
    }

    public bool ContainsItem(PickableItem item)
    {
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                if (_items[y,x].Item == item)
                    return true;
            }
        }

        return false;
    }

    public bool ContainsType(Type type)
    {
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                if (_items[y, x] != null && _items[y, x].Item.GetType() == type)
                    return true;
            }
        }

        return false;
    }

    public IEnumerator<PickableItemInfo> GetEnumerator()
    {
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                yield return _items[y, x];
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private bool TryAllocateItem(PickableItem item, out PickableItemInfo itemInfo)
    {
        itemInfo = null;

        if (IsFull)
        {
            return false;
        }

        if (item.StackBehavior == ItemStackBehavior.Unique && this.Any(t => t?.Item == item))
        {
            return false;
        }

        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                if (_items[y, x] == null)
                {
                    _items[y, x] = itemInfo = new PickableItemInfo(item);
                    return true;
                }
            }
        }

        return true;
    }

    private PickableItemInfo FindStackableItem(PickableItemContainer item)
    {
        foreach (PickableItemInfo itemInfo in this)
        {
            if (item is null)
                continue;

            if (itemInfo.Item == item.Item && itemInfo.Amount + item.Amount <= itemInfo.Item.MaxStackAmount) 
                return itemInfo;
        }

        return null;
    }
}
