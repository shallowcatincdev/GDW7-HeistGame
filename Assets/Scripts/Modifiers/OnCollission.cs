using UnityEngine;
using UnityEngine.Events;

public class OnCollision : MonoBehaviour
{
    // Allows events to trigger when object colides with somthing
    [Header("Item Data")]
    [Tooltip("Optional feild to pass along ItemData when triggered. such as when outputing to a damage modifier")]
    public ItemData data;
    [Header("Output Event")]
    public UnityEvent<ItemData, Collision> onCollission;
    


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");

        if (collision != null)
        {
            onCollission.Invoke(data, collision);
        }

        
    }

   

}
