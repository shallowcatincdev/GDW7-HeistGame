using UnityEngine;

public class InventoryController : MonoBehaviour
{

    public Inventory inv;

    public int currentlyEquipedItem = 0;
    bool holstered = true;
    int maxEquipSlots = 8;

    public ItemInstance NextEquip()
    {
        if (holstered)
        {
            holstered = false;
            return inv.items[currentlyEquipedItem];
        }
        else
        {
            bool itemFound = false;
            int i = 1 + currentlyEquipedItem;
            while (!itemFound)
            {
                Debug.Log(i);
                Debug.Log(inv.items.Count);
                if (i >= inv.items.Count)
                {
                    i = 0;
                }

                if (i - currentlyEquipedItem > maxEquipSlots - 1 || i - currentlyEquipedItem >= inv.items.Count - 1)
                {
                    itemFound = true;
                }
                else if (inv.items[i] == null)
                {
                    i++;
                }
                else
                {
                    currentlyEquipedItem = i;
                    return inv.items[currentlyEquipedItem];
                }
            }
        }

        return null;
    }

    public ItemInstance PrevEquip()
    {
        if (holstered)
        {
            holstered = false;
            return inv.items[currentlyEquipedItem];
        }
        else
        {
            bool itemFound = false;
            int i = 0;
            while (!itemFound)
            {
                if (i > maxEquipSlots - 1)
                {
                    itemFound = true;
                }
                else if (inv.items[i - currentlyEquipedItem] == null)
                {
                    i++;
                    break;
                }
                else
                {
                    currentlyEquipedItem -= i;
                    return inv.items[currentlyEquipedItem];
                }
            }
        }

        return null;
    }

}
