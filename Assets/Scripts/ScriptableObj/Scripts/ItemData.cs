using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
[System.Serializable]
public class ItemData : ScriptableObject
{
    public GameObject prefab;
    public Image inventoryIcon;
    public string uiName;
    public int stackSize;

    public float damageStrength;

    public ItemData()
    {
        
    }

}
