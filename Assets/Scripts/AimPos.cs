using UnityEngine;

public class AimPos : MonoBehaviour
{

    public float maxDistance = 50f;
    public Vector3 aimPoint;
    public Transform shootRef;

    public LayerMask mask;
    GameObject LookingAtObj;
    public ILookedAt lookRef;

    float lookAtMaxDistCache;
    bool calledLookedAt;

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
                    calledLookedAt = false;
                }
                

                if (hit.collider.gameObject.TryGetComponent(out ILookedAt LookedAtObj))
                {
                    lookAtMaxDistCache = LookedAtObj.GetMaxDist();
                    if (hit.distance <  lookAtMaxDistCache || lookAtMaxDistCache == 0)
                    {
                        LookedAtObj.LookedAt();
                        calledLookedAt = true;
                    }
                    lookRef = LookedAtObj;
                }
                LookingAtObj = hit.collider.gameObject;
            }
            else
            {
                if(lookRef != null )
                {
                    if(hit.distance < lookAtMaxDistCache && !calledLookedAt)
                    {
                        lookRef.LookedAt();
                        calledLookedAt = true;
                    }
                    else if (hit.distance >= lookAtMaxDistCache && lookAtMaxDistCache != 0 && calledLookedAt)
                    {
                        lookRef.StopLookedAt();
                        calledLookedAt = false;
                    }
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
                calledLookedAt = false;
            }

            aimPoint = shootRef.position;
            Debug.DrawRay(origin, direction * maxDistance, Color.red);
        }

    }
}
