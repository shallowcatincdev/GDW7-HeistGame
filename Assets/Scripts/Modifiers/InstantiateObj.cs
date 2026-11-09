using UnityEngine;

public class InstantiateObj : MonoBehaviour
{
    public GameObject SpawnObjReturn(GameObject obj, Transform pos)
    {
        return Instantiate(obj, pos.position, transform.rotation);
    }
}
