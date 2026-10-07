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

    public void RemoveItem(ItemInstance itemToRemove)
    {
        items.Remove(itemToRemove);
    }

}
