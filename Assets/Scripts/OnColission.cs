using UnityEngine;
using UnityEngine.Events;

public class OnColission : MonoBehaviour
{
    // Allows events to trigger when object colides with somthing

    public UnityEvent<Collision> onCollission;

    private void OnCollisionEnter(Collision collision)
    {
        onCollission.Invoke(collision);
    }

}
