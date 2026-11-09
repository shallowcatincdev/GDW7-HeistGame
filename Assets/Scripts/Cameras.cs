using System.Collections;
using UnityEngine;

public class Cameras : MonoBehaviour
{
    public float degreesPerSecond = 10f;
    private bool flip;
    private float time = 2.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ReverseSpin(time));
        StartCoroutine(DoubleCamRotation(time));
    }

    // Update is called once per frame
    void Update()
    {
        if (flip == false)
        {
            transform.Rotate(Vector3.forward * (degreesPerSecond * Time.deltaTime));
        }
        else
        {
            transform.Rotate(Vector3.back * (degreesPerSecond * Time.deltaTime));
        }
    }

    IEnumerator ReverseSpin(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        flip = !flip;
        StartCoroutine(ReverseSpin(time));
    }

    IEnumerator DoubleCamRotation(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        degreesPerSecond *= 2;
        StopCoroutine(DoubleCamRotation(time));
    }
}
