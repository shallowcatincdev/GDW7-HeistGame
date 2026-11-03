using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(InstantiateObj))]
public class Interactable : MonoBehaviour
{
    InstantiateObj instObj;

    public GameObject prompt;
    GameObject promptRef;

    public UnityEvent activate;

    public string text;
    bool active;


    private void Start()
    {
        instObj = GetComponent<InstantiateObj>();
    }

    public void SpawnPrompt()
    {
        if (instObj != null && promptRef == null)
        {
            active = true;
            promptRef = instObj.SpawnObjReturn(prompt, transform);
            promptRef.GetComponent<Billboard>().SetText(text);
        }
    }

    public void RemovePrompt()
    {
        if (promptRef != null)
        {
            active = false;
            Destroy(promptRef);
            promptRef = null;
        }
    }

    public void Activate()
    {
        if (promptRef != null && active)
        {
            activate.Invoke();
            RemovePrompt();
        }
    }




}
