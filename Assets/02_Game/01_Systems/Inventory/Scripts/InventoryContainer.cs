using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

public class InventoryContainer
{
    public readonly string ID;
    private readonly List<InventoryItem> _items = new List<InventoryItem>();

    public ReadOnlyCollection<InventoryItem> Items => _items.AsReadOnly();

    public int Capacity => _items.Count;
    public int CurrentCount => _items.Count(item => item != null);
    public bool IsFull => CurrentCount >= Capacity;

    public InventoryContainer(string id, int initialCapacity)
    {
        this.ID = id;
        _items = new List<InventoryItem>(initialCapacity);
        for(int i = 0; i < initialCapacity; i++)
        {
            _items.Add(null);
        }

    }

    public bool PlaceItemAtIndex(int index, InventoryItem item)
    {
        if(!IsValidIndex(index)) return false;

        _items[index] = item;
        return true;
    }

    public bool AddItemToFirtsAvailableSlot(InventoryItem item)
    {
        int availableIndex = _items.IndexOf(null);
        if(availableIndex == -1) return false;

        return PlaceItemAtIndex(availableIndex, item);
    }

    public bool RemoveItem(InventoryItem item)
    {

        int indexToRemove = _items.IndexOf(item);
        if(!IsValidIndex(indexToRemove)) return false;

        return (RemoveItemAtIndex(indexToRemove, out _));
    }
    public bool RemoveItem(InventoryItem item, out InventoryItem itemRemoved)
    {
        itemRemoved = null;
        int indexToRemove = _items.IndexOf(item);
        if(!IsValidIndex(indexToRemove)) return false;

        return (RemoveItemAtIndex(indexToRemove, out itemRemoved));
    }

    public bool RemoveItemAtIndex(int index, out InventoryItem item)
    {
        item = null;
        if(!IsValidIndex(index)) return false;

        item = _items[index];
        _items[index] = null;
        return true;
    }

    public bool SwapItems(int indexA, int indexB)
    {
        InventoryItem itemA = _items[indexA];
        InventoryItem itemB = _items[indexB];

        _items[indexA] = itemB;
        _items[indexB] = itemA;

        return true;
    }

    private bool IsValidIndex(int index)
    {
        return index >= 0 && index < Capacity;
    }

}
