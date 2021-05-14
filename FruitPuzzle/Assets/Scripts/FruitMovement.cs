using System.Collections;
using UnityEngine;

public class FruitMovement : MonoBehaviour
{
    [SerializeField] float gridMovementDuration;

    private bool isGridMoving;

    void Update()
    {

    }

    private IEnumerator GridMovement(Vector3 direction)
    {
        isGridMoving = true;

        float elapsedTime = 0;

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + direction;

        while (elapsedTime < gridMovementDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / gridMovementDuration));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = targetPosition;

        isGridMoving = false;
    }
}
