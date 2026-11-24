using UnityEngine;

public interface ILookedAt
{
    public float GetMaxDist();

    public void LookedAt();

    public void StopLookedAt();

    public void Activate();
}
