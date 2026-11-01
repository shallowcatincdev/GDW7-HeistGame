using UnityEngine;
using UnityEngine.Events;

public class LookingAt : MonoBehaviour, ILookedAt
{
    public UnityEvent lookedAtEvent;
    public UnityEvent stopLookedAtEvent;
    public UnityEvent activateEvent;

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
