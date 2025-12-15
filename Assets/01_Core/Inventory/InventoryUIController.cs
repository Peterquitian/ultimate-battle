using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    [Header("Dependencies")]
    private InventorySystem _inventorySystem;
    private InventoryContainer _container;

    [Header("Controller Config")]
    [SerializeField] private string _containerID = "Inventario";

    [Header("UI References")]
    [SerializeField] private List<SlotView> _controlledSlots = new List<SlotView>();

    private void Awake()
    {
        _inventorySystem = GameInitializer.System;

        if(_inventorySystem == null)
        {
            Debug.LogError("FATAL ERROR: Inventory System reference is missing!");
            return;
        }

        _container = _inventorySystem.GetContainer(_containerID);

        if(_container == null)
        {
            Debug.LogError($"FATAL ERROR: Container ID {_containerID} not found in the system!");
            return;
        }

        InitializeSlots();
    }

    private void OnEnable()
    {
        DropEvents.OnSlotInteractionAttempt += HandleSlotInteraction;
        _inventorySystem.OnContainerChanged += HandleContainerChanged;
    }

    private void OnDisable()
    {
        DropEvents.OnSlotInteractionAttempt -= HandleSlotInteraction;
        _inventorySystem.OnContainerChanged -= HandleContainerChanged;
    }

    private void InitializeSlots()
    {
        for(int i = 0; i < _controlledSlots.Count; i++)
        {
            _controlledSlots[i].SetupSlot(_containerID, i);
        }

        RefreshUI();
    }

    private void HandleSlotInteraction(SlotView source, SlotView destination)
    {
        bool isRelevant = source.ContainerID == _containerID || destination.ContainerID == _containerID;

        if(!isRelevant)
        {
            return;
        }

        if(source.ContainerID != _containerID)
        {
            Debug.Log($"Delegate responsibility at {source.ContainerID}");
            return;
        }

        bool success = _inventorySystem.TryTransferItem(
            source.ContainerID,
            source.SlotIndex,
            destination.ContainerID,
            destination.SlotIndex
        );

        if(!success)
        {
            Debug.LogError($"Error: Transfer failed from {source.ContainerID}: {source.SlotIndex} to {destination.ContainerID}: {destination.SlotIndex}.");
        }

    }

    private void HandleContainerChanged(string changedContainerID)
    {
        if(changedContainerID == _containerID)
        {
            RefreshUI();
        }
    }

    public void RefreshUI()
    {
        var itemsData = _container.Items;

        for(int i = 0; i < _controlledSlots.Count; i++)
        {
            InventoryItem item = null;

            if(i < itemsData.Count)
            {
                item = itemsData[i];
            }

            _controlledSlots[i].UpdateItem(item);
        }
    }


}
