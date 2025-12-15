using System;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem
{
    public event Action<string> OnContainerChanged;

    private readonly Dictionary<string, InventoryContainer> _allContainers = new Dictionary<string, InventoryContainer>();

    public void RegisterContainer(InventoryContainer container)
    {
        if(_allContainers.ContainsKey(container.ID))
        {
            Debug.LogError($"Container with ID {container.ID} already exists!");
            return;
        }

        _allContainers.Add(container.ID, container);
        Debug.Log($"Container '{container.ID}' register with capacity {container.Capacity}.");
    }

    public InventoryContainer GetContainer(string id)
    {
        _allContainers.TryGetValue(id, out InventoryContainer container);
        return container;
    }

    public bool TryAddItem(string destinationContainerID, InventoryItem itemToAdd)
    {
        if(itemToAdd == null
        || !_allContainers.TryGetValue(destinationContainerID, out InventoryContainer container))
        {
            Debug.LogError("Item add failed: Invalid container ID or Item.");
            return false;
        }

        if(!container.AddItemToFirtsAvailableSlot(itemToAdd))
        {
            Debug.LogError($"Item add failed: container {container.ID} is full.");
            return false;
        }

        OnContainerChanged?.Invoke(destinationContainerID);
        return true;

    }

    public bool TryRemoveItem(string destinationContainerID, InventoryItem item)
    {
        if(item == null
        || !_allContainers.TryGetValue(destinationContainerID, out InventoryContainer container))
        {
            Debug.LogError("Item remove failed: Invalid container ID or Item.");
            return false;
        }

        if(!container.RemoveItem(item))
        {
            Debug.LogError($"Item remove failed: item '{item?.name}' not found in container '{container.ID}'");
            return false;
        }

        OnContainerChanged?.Invoke(destinationContainerID);
        return true;
    }
    public bool TryTransferItem(string sourceContainerID, int sourceIndexSlot, string destinationContainerID, int destinationIndexSlot)
    {
        Debug.Log($"Attempting to Transfer: {sourceContainerID}[{sourceIndexSlot}] to {destinationContainerID}[{destinationIndexSlot}]");

        if(!_allContainers.TryGetValue(sourceContainerID, out InventoryContainer source)
        || !_allContainers.TryGetValue(destinationContainerID, out InventoryContainer destination))
        {
            Debug.LogError("Transfer failed: Invalid source or destination container ID");
            return false;
        }

        // Debug.Log("Transfer Item: The IDs of the containers successfully checked.");

        if(!source.RemoveItemAtIndex(sourceIndexSlot, out InventoryItem removedSourceItem))
        {
            Debug.LogError("Transfer failed: Invalid source index");
            return false;
        }

        // Debug.Log($"Transfer Item: Item: {removedSourceItem?.name} removed for {source.ID}[{sourceIndexSlot}] successfully.");

        if(!destination.RemoveItemAtIndex(destinationIndexSlot, out InventoryItem removedDestinationItem))
        {
            source.PlaceItemAtIndex(sourceIndexSlot, removedSourceItem);
            Debug.LogError("Transfer failed: Invalid destination index");
            return false;
        }

        // Debug.Log(removedDestinationItem);
        // Debug.Log(destination);
        // Debug.Log(destinationIndexSlot);
        
        // Debug.Log($"Transfer Item: Item: {removedDestinationItem?.name} removed for {destination.ID}[{destinationIndexSlot}] successfully.");

        source.PlaceItemAtIndex(sourceIndexSlot, removedDestinationItem);
        destination.PlaceItemAtIndex(destinationIndexSlot, removedSourceItem);

        // Debug.Log($"Transfer Item: successfully!!");

        OnContainerChanged?.Invoke(sourceContainerID);
        if(sourceContainerID != destinationContainerID)
        {
            OnContainerChanged?.Invoke(destinationContainerID);
        }
        
        return true;
    }

}
