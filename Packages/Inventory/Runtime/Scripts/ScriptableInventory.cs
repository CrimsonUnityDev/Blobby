using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu] 
public class ScriptableInventory : ScriptableObject
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private List<InventorySlot> slots = new List<InventorySlot>();
    public delegate void InventoryEvent(Inventory inv);
    public event InventoryEvent InventoryChanged;

    public Inventory Get(List<InventorySlot> defaultSlots = null)
    {
        if (inventory==null)
        {
            inventory = new Inventory();
            inventory.InventoryChanged += HandleInventoryChanged;
            if (defaultSlots!=null)
            {
                inventory.Recieve(defaultSlots);
            }
            inventory.Recieve(slots);
            return inventory;
        }
        else
        {
            return inventory;
        }
    }

    private void HandleInventoryChanged(Inventory inv)
    {
        if (InventoryChanged!=null)
        {
            InventoryChanged(inv);
        }
    }

    private void OnDestroy()
    {
        if (inventory!=null)
        {
            inventory.InventoryChanged -= HandleInventoryChanged;
        }
    }
}
