using UnityEngine;

public class GridParticleEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem chocolateSplash;
    [SerializeField] int emitAmount;

    private void OnTriggerEnter(Collider other)
    {
        chocolateSplash.Emit(emitAmount);
    }
}
