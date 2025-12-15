using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class SlotView : MonoBehaviour
{
    [Header("Visual Components")]
    [SerializeField] private Image _itemIcon;
    [SerializeField] private Image _SloctIcon;
    [SerializeField] GameObject _emptyIndicator;

    public string ContainerID {get; private set;}
    public int SlotIndex {get; private set;}
    public InventoryItem CurrentItem {get; private set;}
    public bool IsEmpty => CurrentItem == null;

    public void SetupSlot(string containerID, int index)
    {
        ContainerID = containerID;
        SlotIndex = index;

    }

    public void UpdateItem(InventoryItem itemData)
    {
        CurrentItem = itemData;

        if(CurrentItem != null)
        {
            _itemIcon.sprite = CurrentItem.ItemIcon;
            _itemIcon.enabled = true;

            if(_emptyIndicator != null) _emptyIndicator.SetActive(false);
        }

        else
        {
            _itemIcon.sprite = null;
            _itemIcon.enabled = false;

            if(_emptyIndicator != null) _emptyIndicator.SetActive(true);
        }
        
    }

    public void SetTransparency(float value)
    {
        _itemIcon.color = new Color (1.0f,1.0f,1.0f, value);
    }

    public void SetColor(Color color)
    {
        _itemIcon.color = color;
    }
}
