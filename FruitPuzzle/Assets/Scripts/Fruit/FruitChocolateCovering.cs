using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class FruitChocolateCovering : MonoBehaviour
{
    [SerializeField] GameObject topCover, bottomCover, leftCover, rightCover;
    [SerializeField] LayerMask chocolateGridLayer;
    [SerializeField] float raycastLength;

    private bool isTopCovered, isBottomCovered, isLeftCovered, isRightCovered;

    private List<bool> surfacesToCover;

    private bool isFullCovered;

    private void Awake()
    {
        surfacesToCover = new List<bool>() { isTopCovered, isBottomCovered, isLeftCovered, isRightCovered };
    }

    void Update()
    {
        if (!isFullCovered)
        {
            if (Physics.Raycast(transform.position, transform.up, raycastLength, chocolateGridLayer))
            {
                topCover.SetActive(true);
                surfacesToCover[0] = true;
            }
            if (Physics.Raycast(transform.position, -transform.up, raycastLength, chocolateGridLayer))
            {
                bottomCover.SetActive(true);
                surfacesToCover[1] = true;            }
            if (Physics.Raycast(transform.position, -transform.right, raycastLength, chocolateGridLayer))
            {
                leftCover.SetActive(true);
                surfacesToCover[2] = true;
            }
            if (Physics.Raycast(transform.position, transform.right, raycastLength, chocolateGridLayer))
            {
                rightCover.SetActive(true);
                surfacesToCover[3] = true;
            }

            CheckAllSurfacesAreCovered();
        }
    }

    private void CheckAllSurfacesAreCovered()
    {
        if (surfacesToCover.All(x => x == true))
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
