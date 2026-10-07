using UnityEngine;

public class AimPos : MonoBehaviour
{

    public float maxDistance = 50f;
    public Vector3 aimPoint;

    void Update()
    {
        // 1. Define the origin point and the direction of the ray
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward; // Fires straight ahead

        // 2. Create a variable to hold the information about what we hit
        RaycastHit hit;

        // 3. Perform the Raycast
        if (Physics.Raycast(origin, direction, out hit, maxDistance))
        {
            aimPoint = hit.point;

        }

    }
}
