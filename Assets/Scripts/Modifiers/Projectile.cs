
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject projectile;
    
    public GameObject SpawnProjectile(Transform _transform, float speed)
    {
        var obj = Instantiate(projectile, _transform.position, _transform.rotation);


        obj.GetComponent<Rigidbody>().AddForce(_transform.forward * speed);

        return obj;
    }

    
}
