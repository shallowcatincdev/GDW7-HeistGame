using UnityEngine;
using UnityEngine.Events;

public class OnTrigger : MonoBehaviour
{
    [Header("Item Data")]
    [Tooltip("Optional feild to pass along ItemData when triggered. such as when outputing to a damage modifier")]
    public ItemData data;
    [Header("Output Event")]
    public UnityEvent<ItemData, Collider> onTrigger;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");

        if (other != null)
        {
            onTrigger.Invoke(data, other);
        }


    }
}
