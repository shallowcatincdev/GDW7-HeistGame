using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float hp = 100;
    public UnityEvent onHealthZero;
    public void ChangeHealth(float amountToChange)
    {
        hp += amountToChange;

        if (hp <= 0)
        {
            onHealthZero.Invoke();
        }

    }
}
