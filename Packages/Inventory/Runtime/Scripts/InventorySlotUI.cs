using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMPro.TextMeshProUGUI text;
    [SerializeField] private TMPro.TextMeshProUGUI countText;
    [SerializeField] private InventoryItemType type;
    [SerializeField] private ScriptableInventory scriptableInventory;

    private void Awake()
    {
        scriptableInventory.InventoryChanged += Configure;
    }
    private void OnDestroy()
    {
        scriptableInventory.InventoryChanged -= Configure;
    }

    private void Start()
    {
        if (scriptableInventory)
        {
            Configure(scriptableInventory.Get());
        }
    }
    public void Configure(Inventory inv)
    { 
        if (image)
        {
            image.sprite = type.uiSprite;
        }
        if (text)
        {
            text.text = type.itemName;
        }
        if (countText)
        {
            if ( inv !=null && inv.contents!=null)
            {
                if (inv.contents.ContainsKey(type))
                {
                    countText.text = inv.contents[type].ToString();
                    return;
                }
            }
            countText.text = "";
        }
    }
}
