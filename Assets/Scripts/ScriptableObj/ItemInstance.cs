using UnityEngine;

[System.Serializable]
public class ItemInstance
{
    public ItemData itemType;
    public int condition;

    public ItemInstance(ItemData itemData)
    {
        itemType = itemData;
        condition = 100;

        if (itemData is GunData)
        {

        }
    }

}
