using UnityEngine;

[RequireComponent(typeof(InstantiateObj))]
public class Explosive : MonoBehaviour
{
    InstantiateObj instObj;
    public GameObject ExplosiveEffect;

    private void Awake()
    {
        instObj = GetComponent<InstantiateObj>();
    }


    public void Explode()
    {
        instObj.SpawnObjReturn(ExplosiveEffect, transform);
    }



    
    
        

}
