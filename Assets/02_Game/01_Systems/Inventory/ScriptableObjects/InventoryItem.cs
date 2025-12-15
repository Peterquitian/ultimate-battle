using UnityEngine;

public class InventoryItem : ScriptableObject
{
    [Header("Item Metadata")]
    public string ItemId;
    public string ItemName;
    public Sprite ItemIcon;
    public string ItemDescription;

    [Header("Shop/Value")]
    public int BasePrice;
    public bool IsStackable = false;
    public void UseItem(){}     

}
