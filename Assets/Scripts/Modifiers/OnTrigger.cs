using UnityEngine;
using UnityEngine.Events;

public class OnTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public UnityEvent<ItemData, Collider> onTrigger;
    public ItemData data;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");

        if (other != null)
        {
            onTrigger.Invoke(data, other);
        }


    }
}
