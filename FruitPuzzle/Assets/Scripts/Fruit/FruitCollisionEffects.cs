using UnityEngine;

public enum FruitSide
{
    Top,
    Bottom,
    Left,
    Right
}

public class FruitCollisionEffects : MonoBehaviour
{
    [SerializeField] FruitSide side;

    [SerializeField] ParticleSystem chocolateSpash;
    [SerializeField] ParticleSystem chocolateTrace;

    [SerializeField] Vector3 particleOffset;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ChocolateGridBlock"))
        {
            PlayParticle(chocolateSpash);           
        }
        else if (other.gameObject.CompareTag("EmptyGridBlock"))
        {
            if (side == FruitSide.Top && FruitChocolateCovering.isTopCovered)
            {
                PlayParticle(chocolateTrace);
            }
            else if (side == FruitSide.Bottom && FruitChocolateCovering.isBottomCovered)
            {
                PlayParticle(chocolateTrace);
            }
            else if (side == FruitSide.Left && FruitChocolateCovering.isLeftCovered)
            {
                PlayParticle(chocolateTrace);
            }
            else if (side == FruitSide.Right && FruitChocolateCovering.isRightCovered)
            {
                PlayParticle(chocolateTrace);
            }
            else
            {
                return;
            }
        }
    }

    private void PlayParticle(ParticleSystem desiredParticle)
    {
        desiredParticle.transform.position = transform.position + particleOffset;
        desiredParticle.transform.rotation = transform.rotation;
        desiredParticle.Play();
    }
}
