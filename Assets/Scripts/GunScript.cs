using UnityEngine;
using UnityEngine.Events;

public class GunScript : WeaponScript
{
    ///
    /// This Script acts as the base for all gun based weapons.
    /// all additional modification should be done through additional scripts through script composition
    ///

    public GunData data;
    public Projectile projectileScript;
    public GameObject shootPoint;

  

    public override void PrimaryAction()
    {
        if (itemInstance.ammoCount > 0)
        {
            projectileScript.SpawnProjectile(shootPoint.transform, data.bulletSpeed);
            itemInstance.ammoCount--;
        }
        else
        {
            ReloadGun();
        }
    }

    public override void SecondaryAction()
    {
        Debug.Log("Gun Secondary Action"); // TODO add scope-in Function
    }
   
    public void ReloadGun()
    {
        itemInstance.ammoCount = data.magCapacity; // TEMP in future check total ammo levels, remove ammo from inventory, etc...
    }

}
