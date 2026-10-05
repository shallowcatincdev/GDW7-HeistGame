using UnityEngine;

public class WeaponScript : ItemScript
{
    ///
    /// This script (a child of ItemScript) will be used as reference for all items concidered weapons.
    ///

    public virtual void PrimaryAction()
    {
        
    }

    public virtual void SecondaryAction()
    {

    }

    public void OnAttack() // TEMP - Move to player controller later
    {
        PrimaryAction();
    }

}
