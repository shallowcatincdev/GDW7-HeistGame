using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    public GameObject cameraTarget;
    public float speed = 5f;
    private Vector3 currentVelocity = Vector3.zero;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 newPos = Vector3.Lerp(transform.position, cameraTarget.transform.position, speed * Time.deltaTime);
        rb.MovePosition(newPos);
        transform.rotation = cameraTarget.GetComponentInParent<Transform>().rotation;

         
    }

}
