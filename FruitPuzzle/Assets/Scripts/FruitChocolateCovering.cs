using UnityEngine;

public class FruitChocolateCovering : MonoBehaviour
{
    [SerializeField] GameObject topCover, bottomCover, leftCover, rightCover;
    [SerializeField] LayerMask chocolateGridLayer;
    [SerializeField] float raycastLength;

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.up, raycastLength, chocolateGridLayer))
        {
            topCover.SetActive(true);
        }
        if (Physics.Raycast(transform.position, -transform.up, raycastLength, chocolateGridLayer))
        {
            bottomCover.SetActive(true);
        }
        if (Physics.Raycast(transform.position, -transform.right, raycastLength, chocolateGridLayer))
        {
            leftCover.SetActive(true);
        }
        if (Physics.Raycast(transform.position, transform.right, raycastLength, chocolateGridLayer))
        {
            rightCover.SetActive(true);
        }
    }
}
