using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public ItemInstance inst;
    public InventoryController inventoryController;

    public void PickupItem()
    {

        inventoryController.inv.AddItem(inst);

        Destroy(gameObject);


    }
}
