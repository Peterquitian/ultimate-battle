using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameInitializer : MonoBehaviour
{
    public static InventorySystem System {get; private set;}

    [Header("Configuración Inicial")]
    [SerializeField] private int _initialShopCapacity = 10;
    [SerializeField] private int _initialBackpackCapacity = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(System == null)
        {
            System = new InventorySystem();
        }

        InitializeContainers();
        Debug.Log("Inventory System initialized and containers registered.");
        
    }

    private void InitializeContainers()
    {
        InventoryContainer backpack = new InventoryContainer("Backpack", _initialBackpackCapacity);
        System.RegisterContainer(backpack);

        InventoryContainer belt = new InventoryContainer("Belt", _initialShopCapacity);
        System.RegisterContainer(belt);
    }

}
