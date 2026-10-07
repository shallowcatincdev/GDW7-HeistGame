using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public GameObject prefab;
    public Image inventoryIcon;
    public int stackSize;

    public float damageStrength;

}
