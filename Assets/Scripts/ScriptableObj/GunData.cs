using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ItemData
{
    public float bulletSpeed;
    public int magCapacity;
    public int ammoType;
    public GameObject bulletPrefab;

}
