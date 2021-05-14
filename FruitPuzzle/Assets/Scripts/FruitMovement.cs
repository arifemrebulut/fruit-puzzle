using System.Collections;
using UnityEngine;

public class FruitMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float swipeDelta;

    private bool isTouching;

    private Vector3 touchStartPosition;

    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        if (!isTouching && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            touchStartPosition = Input.touches[0].position;
            isTouching = true;
        }

        if (isTouching)
        {
            if (Input.GetTouch(0).position.y >= touchStartPosition.y + swipeDelta)
            {
                isTouching = false;
                SetTargetPosition(Vector3.forward);
            }
            else if (Input.GetTouch(0).position.y <= touchStartPosition.y - swipeDelta)
            {
                isTouching = false;
                SetTargetPosition(Vector3.back);
            }
        }

        MoveFruit();
    }

    private void SetTargetPosition(Vector3 direction)
    {
        targetPosition += direction;
    }

    private void MoveFruit()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed);
    }
}
