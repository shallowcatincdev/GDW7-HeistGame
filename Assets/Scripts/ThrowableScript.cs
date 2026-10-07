using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.PlayerSettings;
using static UnityEngine.Rendering.DebugUI.Table;

public class ThrowableScript : WeaponScript
{
    ///
    /// This Script acts as the base for all Throwable weapons.
    /// all additional modification should be done through additional scripts through script composition
    ///

    public ThrowableData data;
    public Projectile projectileScript;
    public GameObject throwPoint;

    public override void PrimaryAction()
    {
        if (true)
        {
            projectileScript.SpawnProjectile(throwPoint.transform, data.ThrowSpeed);
        }
    }

    public override void SecondaryAction()
    {
        Debug.Log("Throwable Secondary Action");
    }

    

}
