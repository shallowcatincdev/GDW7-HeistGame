using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public ItemInstance inst;
    public InventoryController inventoryController;

    public bool PickupItem()
    {
        bool result;

        result = inventoryController.inv.AddItem(inst);
        if (result)
        {
            Destroy(gameObject);
            return result;
        }
        else
        {
            return result;
        }
    }
}
