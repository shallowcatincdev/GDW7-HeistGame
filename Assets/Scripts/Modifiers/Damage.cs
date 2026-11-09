using UnityEngine;

public class Damage : MonoBehaviour
{
    public void DoDamage(ItemData data, GameObject obj)
    {
        DamageObj(obj, data);
    }

    public void DoDamage(ItemData data, Collision col)
    {
        GameObject obj = col.gameObject;

        DamageObj(obj, data);

    }

    public void DoDamage(ItemData data, Collider cod)
    {
        GameObject obj = cod.gameObject;

        DamageObj(obj, data);

    }


    void DamageObj(GameObject obj, ItemData data) 
    {
        Debug.Log("Damage");
        if (data == null)
        {

        }
        else if (data.damageStrength <= 0)
        {

        }
        else if (obj.TryGetComponent(out Damageable damageRef))
        {
            damageRef.DealDamage(data.damageStrength);

        }
        else
        {

        }
    }

}
