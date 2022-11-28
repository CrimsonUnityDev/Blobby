using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPickup : MonoBehaviour
{
    public List<InventorySlot> data = new List<InventorySlot>();

    private void OnTriggerEnter(Collider other)
    {
        InventoryController controller = other.GetComponent<InventoryController>();
        if (controller)
        {
            controller.Receieve(data);
            Destroy(this.gameObject);
        }
    }

}
