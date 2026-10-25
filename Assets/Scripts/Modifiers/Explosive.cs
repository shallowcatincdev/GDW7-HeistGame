using UnityEngine;

public class Explosive : MonoBehaviour
{
    public ParticleSystem ExplosiveEffect;

    public void Explode()
    {
        ExplosiveEffect.Play();
    }
}
