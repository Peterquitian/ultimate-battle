using UnityEngine;

public class AddItem : MonoBehaviour
{
    [Header("Test Inventary")]
    [SerializeField] private string _inventoryID;

    [Header("Add Item")]
    [SerializeField] private InventoryItem _item;
    [SerializeField] private bool _addItem;

    [Header("Print Invetary")]
    [SerializeField] private bool _printInventary;
    
    private InventorySystem _system;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _addItem = false;
        _system = GameInitializer.System;
    }

    // Update is called once per frame
    void Update()
    {
        if(_addItem)
        {
            _addItem = false;
            TryAddItem();
        }
        if(_printInventary)
        {
            _printInventary = false;
            PrintInvetary();
        }
    }

    public bool TryAddItem()
    {
        if(_item == null)
        {
            Debug.LogError($"Test: There are no item to add");
            return false;
        }

        if(_inventoryID == "" || _inventoryID == null)
        {
            Debug.LogError($"Test: Inventory no selected");
            return false;
        }

        if(!_system.TryAddItem(_inventoryID, _item))
        {
            Debug.LogError($"Test: Error adding the item");
            return false;
        }

        Debug.Log("Test: Test Item successfully added");

        return false;
    }

    public void PrintInvetary()
    {
        if(_inventoryID == "" || _inventoryID == null) return;
        Debug.Log($"{_inventoryID}: number of items in the invetory: {_system?.GetContainer(_inventoryID).CurrentCount}\n {_system?.GetContainer(_inventoryID).Items.ToString()}");
    }
}
