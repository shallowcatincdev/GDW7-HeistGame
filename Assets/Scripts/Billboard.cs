using System;
using TMPro;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera cam;
    public TextMeshPro actionText;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }


    private void LateUpdate()
    {
        if (cam != null)
        {
            transform.forward = cam.transform.forward;
        }
    }

    public void SetText(string text)
    {
        if (actionText != null)
        {
            actionText.text = text;
        }
    }

}
