using UnityEngine;

public class FruitCollisionVFX : MonoBehaviour
{
    [SerializeField] ParticleSystem chocolateSpash;
    [SerializeField] ParticleSystem chocolateTrace;

    [SerializeField] Vector3 particleOffset;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ChocolateGridBlock"))
        {
            ParticleSystem particle = Instantiate(chocolateSpash, transform);
            particle.transform.position = transform.position + particleOffset;
            particle.transform.rotation = transform.rotation;
            particle.Play();
        }
    }
}
