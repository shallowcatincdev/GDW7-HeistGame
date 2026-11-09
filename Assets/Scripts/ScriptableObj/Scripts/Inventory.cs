using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable Objects/Inventory")]
public class Inventory : ScriptableObject
{
    public List<ItemInstance> items = new();
    public int maxItems = 10;
    public bool AddItem(ItemInstance itemToAdd)
    {
        for (int i = 0; i < items.Count; i++) // check for empty slots
        {
            if (items[i] == null)
            {
                items[i] = itemToAdd;
                return true;
            }
        }

        if (items.Count < maxItems) // check for remaining space
        {
            items.Add(itemToAdd);
            return true;
        }

        Debug.Log("No space in the inventory");
        return false;
    }

    public int AddItem(ItemInstance itemToAdd, int targetIndex)
    {
        if (targetIndex > items.Count)
        {
            return -1; // Not in range
        }
        else if (items[targetIndex] != null)
        {
            return -2; // slot full TODO add check to add item stacking if supported
        }
        else if (itemToAdd == null)
        {
            return -3; // No item provided
        }
        else
        {
            items.Insert(targetIndex, itemToAdd);
            return 1;
        }
    }

    public void SwapItem(int indexOne, int indexTwo)
    {
        ItemInstance tempSlot = items[indexOne];
        RemoveItem(indexOne);
        AddItem(items[indexTwo], indexOne);
        RemoveItem(indexTwo);
        AddItem(tempSlot, indexTwo);
        tempSlot = null;
    }

    public void RemoveItem(ItemInstance itemToRemove)
    {
        items.Remove(itemToRemove);
    }

    public void RemoveItem(int index)
    {
        items.RemoveAt(index);
    }

    public ItemInstance GetItem(int index)
    {
        return items[index];
    }

    public int GetIndex(ItemInstance item)
    {
        return items.IndexOf(item);
    }

}
