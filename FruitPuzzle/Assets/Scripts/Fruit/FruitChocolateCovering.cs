using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class FruitChocolateCovering : MonoBehaviour
{
    [SerializeField] GameObject topCover, bottomCover, leftCover, rightCover;
    [SerializeField] LayerMask chocolateGridLayer;
    [SerializeField] float raycastLength;

    private bool isTopCovered, isBottomCovered, isLeftCovered, isRightCovered;

    private List<bool> coveredSurfaces;

    private bool isFullCovered;

    private void Awake()
    {
        coveredSurfaces = new List<bool>() { isTopCovered, isBottomCovered, isLeftCovered, isRightCovered};
    }

    void Update()
    {
        if (!isFullCovered)
        {
            if (Physics.Raycast(transform.position, transform.up, raycastLength, chocolateGridLayer))
            {
                topCover.SetActive(true);
                isTopCovered = true;
                coveredSurfaces[0] = isTopCovered;
            }
            if (Physics.Raycast(transform.position, -transform.up, raycastLength, chocolateGridLayer))
            {
                bottomCover.SetActive(true);
                isBottomCovered = true;
                coveredSurfaces[1] = isBottomCovered;
            }
            if (Physics.Raycast(transform.position, -transform.right, raycastLength, chocolateGridLayer))
            {
                leftCover.SetActive(true);
                isLeftCovered = true;
                coveredSurfaces[2] = true;
            }
            if (Physics.Raycast(transform.position, transform.right, raycastLength, chocolateGridLayer))
            {
                rightCover.SetActive(true);
                isRightCovered = true;
                coveredSurfaces[3] = true;
            }

            CheckAllSurfacesAreCovered();
        }
    }

    private void CheckAllSurfacesAreCovered()
    {
        if (coveredSurfaces.All(x => x == true))
        {
            EventBroker.CallOnLevelComplete();
            isFullCovered = true;
        }
    }

    public void FinishLevel()
    {
        EventBroker.CallOnLevelComplete();
        isFullCovered = true;
    }
}
