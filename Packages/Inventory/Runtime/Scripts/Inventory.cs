using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Inventory 
{
    public delegate void InventoryEvent(Inventory inv);
    public event InventoryEvent InventoryChanged;

    public Dictionary<InventoryItemType, int> contents = new Dictionary<InventoryItemType, int>();

    private void TriggerInventoryChanged(Inventory inv)
    {
        if (InventoryChanged!=null)
        {
            InventoryChanged(inv);
        }
    }
    public void Recieve(InventoryItemType itemType, int count)
    {
        if (count != 0)
        {
            if (contents.ContainsKey(itemType))
            {
                contents[itemType] += count;
            }
            else
            {
                contents.Add(itemType, count);
            }
            TriggerInventoryChanged(this);
        }
    }

    public void Recieve(List<InventorySlot> items)
    {
        if (items.Any(x=> x.count !=0 ))
        {
            for (int i=0; i< items.Count; i++)
            {
                if (contents.ContainsKey(items[i].itemType ))
                {
                    contents[items[i].itemType] += items[i].count;
                }
                else 
                {
                    contents.Add(items[i].itemType, items[i].count);
                }
            }
            TriggerInventoryChanged(this);
        }
    }

    public void Clear(InventoryItemType type, bool remove =false)
    {
        if (contents.ContainsKey(type) && contents[type] != 0)
        {
            contents[type] = 0;
            if (remove)
            {
                contents.Remove(type);
            }
            TriggerInventoryChanged(this);
        }
    }

    public void Clear()
    {
        contents.Clear();
        TriggerInventoryChanged(this);
    }
}
