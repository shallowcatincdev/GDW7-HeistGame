using UnityEngine;

public class Damage : MonoBehaviour
{
    public void DoDamage(GameObject obj, float damage)
    {

    }

    public void DoDamage(Collision col, float damage)
    {
        GameObject obj = col.gameObject;

        

    }


    void DamageObj(GameObject obj, float damage) 
    {
        
    }

}
