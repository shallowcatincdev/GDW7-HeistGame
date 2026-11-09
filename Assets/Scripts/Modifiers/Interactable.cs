using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(InstantiateObj))]
public class Interactable : MonoBehaviour, ILookedAt
{
    [Header("Obj Refrences")]
    [Tooltip("Refrence to the prompt prefab.")]
    public GameObject prompt;
    GameObject promptRef;
    InstantiateObj instObj;

    [Header("Settings")]
    [Tooltip("Text to display in prompt")]
    public string text;
    [Tooltip("Max distance for the prompt to appear. (note distance from camera not player)")]
    public float maxDist;
    [Tooltip("If enabled prompt gets removed when used. Disable for actions that are inteanded to be called multiple times.")]
    public bool removePromptWhenTriggered = true;

    [Header("Output Event")]
    [Tooltip("Event will be called when interacted with.")]
    public UnityEvent activate;
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

            if (removePromptWhenTriggered)
            {
                RemovePrompt();
            }
            
        }
    }


    public float GetMaxDist()
    {
        return maxDist;
    }

    public void LookedAt()
    {
        SpawnPrompt();
    }

    public void StopLookedAt()
    {
        RemovePrompt();
    }

}
