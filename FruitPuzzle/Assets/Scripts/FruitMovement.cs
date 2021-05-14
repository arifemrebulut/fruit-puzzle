using System.Collections;
using UnityEngine;

public class FruitMovement : MonoBehaviour
{
    [SerializeField] float gridMovementDuration;

    private PlayerInput playerInput;

    private bool isGridMoving;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        if (playerInput.swipeDirection == Vector3.up)
        {
            StartCoroutine(GridMovement(Vector3.up));
        }
        else if (playerInput.swipeDirection == Vector3.down)
        {
            StartCoroutine(GridMovement(Vector3.down));
        }
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
