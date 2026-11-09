using UnityEngine;
using UnityEngine.Events;

public class LookingAt : MonoBehaviour, ILookedAt
{
    public float maxDist = 0;

    public UnityEvent lookedAtEvent;
    public UnityEvent stopLookedAtEvent;
    public UnityEvent activateEvent;

    public float GetMaxDist()
    {
        return maxDist;
    }

    public void LookedAt()
    {
        lookedAtEvent.Invoke();
    }

    public void StopLookedAt()
    {
        stopLookedAtEvent.Invoke();
    }

    public void Activate()
    {
        activateEvent.Invoke();
    }
}
