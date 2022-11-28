using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField] private List<InventorySlot> defaultLocalSlots;
    [SerializeField] private ScriptableInventory scriptableInventory;

    private void Awake()
    {
        if (scriptableInventory)
        {
            inventory = scriptableInventory.Get(defaultLocalSlots);
        }
        else
        {
            inventory = new Inventory();
        }
    }

    public void Recieve(InventoryItemType itemType, int count)
    {
        inventory.Recieve(itemType, count);
    }

    public void Receieve(List<InventorySlot> slots)
    {
        inventory.Recieve(slots);
    }
}
