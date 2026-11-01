using UnityEngine;

public class AimPos : MonoBehaviour
{

    public float maxDistance = 50f;
    public Vector3 aimPoint;
    public Transform shootRef;

    public LayerMask mask;
    GameObject LookingAtObj;
    public ILookedAt lookRef;

    void Update()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(origin, direction, out hit, maxDistance, mask))
        {
            aimPoint = hit.point;
            Debug.DrawRay(origin, direction * hit.distance, Color.green);

            if (LookingAtObj != hit.collider.gameObject)
            {
                if (lookRef != null)
                {
                    lookRef.StopLookedAt();
                    lookRef = null;
                    LookingAtObj = null;
                }
                

                if (hit.collider.gameObject.TryGetComponent(out ILookedAt LookedAtObj))
                {
                    
                    LookedAtObj.LookedAt();
                    lookRef = LookedAtObj;
                    LookingAtObj = hit.collider.gameObject;

                }
            }
            

        }
        else
        {
            if (lookRef != null)
            {
                lookRef.StopLookedAt();
                lookRef = null;
                LookingAtObj = null;
            }

            aimPoint = shootRef.position;
            Debug.DrawRay(origin, direction * maxDistance, Color.red);
        }

    }
}
