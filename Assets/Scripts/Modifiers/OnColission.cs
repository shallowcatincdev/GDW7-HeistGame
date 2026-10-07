using UnityEngine;
using UnityEngine.Events;

public class OnCollision : MonoBehaviour
{
    // Allows events to trigger when object colides with somthing

    public UnityEvent<ItemData, Collision> onCollission;
    public ItemData data;

    private void OnCollisionEnter(Collision collision)
    {
        if (data == null)
        {
            onCollission.Invoke(data, collision);
        }

        
    }

}
