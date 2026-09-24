using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.PlayerSettings;
using static UnityEngine.Rendering.DebugUI.Table;

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
        if (true)
        {
            projectileScript.SpawnProjectile(shootPoint.transform, data.bulletSpeed);
        }
    }

    public override void SecondaryAction()
    {
        Debug.Log("Gun Secondary Action");
    }

    public void OnAttack()
    {
        PrimaryAction();
    }
   
}
