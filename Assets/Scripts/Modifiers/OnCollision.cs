using UnityEngine;
using UnityEngine.Events;

public class OnColission : MonoBehaviour
{
    // Allows events to trigger when object colides with somthing

    public UnityEvent<Collision, ItemData> onCollission;
    public ItemData data;

    private void OnCollisionEnter(Collision collision)
    {
        if (data == null)
        {
            onCollission.Invoke(collision, data);
        }

        
    }

}
