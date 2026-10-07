using System.Collections;
using UnityEngine;

public class Marking : MonoBehaviour
{
    public Camera cam;

    private GameObject enemy;
    private Material oldMaterial;
    public Material markedMaterial;
    
    
    private bool canMark = false;
    private bool isMarked = false;

    private float range = 100f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void MarkEnemy()
    {
        Vector3 rayOrigin = cam.ViewportToWorldPoint (new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit, range))
        {
            //Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.gameObject.tag == "Enemy" && isMarked == false)
            {
                enemy = hit.collider.gameObject;
                oldMaterial = enemy.GetComponent<Renderer>().material;
                enemy.gameObject.gameObject.GetComponent<MeshRenderer>().material = markedMaterial;
                isMarked = true;
                StartCoroutine(Marked());
                Debug.Log("Change Colour");
            }
        }
        
    }

    IEnumerator Marked()
    {
        yield return new WaitForSeconds(1.5f);
        enemy.gameObject.gameObject.GetComponent<MeshRenderer>().material = oldMaterial;
        
        isMarked = false;
    }
}
