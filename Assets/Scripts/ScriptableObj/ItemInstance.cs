using JetBrains.Annotations;
using UnityEngine;

[System.Serializable]
public class ItemInstance
{
    public ItemData itemData;
    public GunData gunData;
    public bool isGunType;
    public int ammoCount;

    public Texture2D icon;
    public string uiName;


    public ItemInstance(ItemData _itemData)
    {
        itemData = _itemData;

        if (itemData is GunData)
        {
            isGunType = true;
            gunData = (GunData)itemData;
            ammoCount = 0;
        }
    }

}
