using System.Collections;
using UnityEngine;

public class FruitMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float flipSpeed;
    [SerializeField] float swipeDelta;

    private bool isTouching;
    private bool isFlipping;

    private Vector3 touchStartPosition;
    private Vector3 movementTargetPosition;

    FruitGridCheck fruitGridCheck;

    private void Start()
    {
        fruitGridCheck = GetComponent<FruitGridCheck>();
        movementTargetPosition = transform.position;
    }

    void Update()
    {
        if (!isTouching && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            touchStartPosition = Input.touches[0].position;
            isTouching = true;
        }

        if (isTouching && Input.touchCount > 0)
        {
            if (Input.GetTouch(0).position.y >= touchStartPosition.y + swipeDelta)
            {
                if (fruitGridCheck.CanMoveForward())
                {
                    isTouching = false;
                    SetTargetPosition(Vector3.forward);
                }
                else
                {
                    isTouching = false;
                    CantMove(Vector3.forward);
                }
            }
            else if (Input.GetTouch(0).position.y <= touchStartPosition.y - swipeDelta)
            {
                if (fruitGridCheck.CanMoveBack())
                {
                    isTouching = false;
                    SetTargetPosition(Vector3.back);
                }
                else
                {
                    isTouching = false;
                    CantMove(Vector3.back);
                }
            }
            else if (Input.GetTouch(0).position.x >= touchStartPosition.x + swipeDelta)
            {
                isTouching = false;
                if (fruitGridCheck.CanMoveRight())
                {
                    if (!isFlipping)
                    {
                        StartCoroutine(FlipFruit(Vector3.right));
                    }
                }
                else
                {
                    CantMove(Vector3.right);
                }
            }
            else if (Input.GetTouch(0).position.x <= touchStartPosition.x - swipeDelta)
            {
                isTouching = false;
                if (fruitGridCheck.CanMoveLeft())
                {
                    if (!isFlipping)
                    {
                        StartCoroutine(FlipFruit(Vector3.left));
                    }
                }
                else
                {
                    CantMove(Vector3.left);
                }
            }
        }

        if (!isFlipping)
        {
            MoveFruit();
        }
    }

    private void SetTargetPosition(Vector3 direction)
    {
        movementTargetPosition += direction;
        EventBroker.CallOnJump();
    }

    private void CantMove(Vector3 direction)
    {
        EventBroker.CallOnCantJump(direction);
    }

    private void MoveFruit()
    {
        transform.position = Vector3.MoveTowards(transform.position, movementTargetPosition, movementSpeed);
    }

    private IEnumerator FlipFruit(Vector3 flipDirection)
    {
        isFlipping = true;

        Vector3 rotAxis = Vector3.Cross(Vector3.up, flipDirection);
        Vector3 pivot = (transform.position + Vector3.down * 0.5f) + flipDirection * 0.5f;

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.AngleAxis(90.0f, rotAxis) * startRotation;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = transform.position + flipDirection;

        float rotSpeed = 90.0f / flipSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < flipSpeed)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime < flipSpeed)
            {
                transform.RotateAround(pivot, rotAxis, rotSpeed * Time.deltaTime);
                yield return null;
            }
        }

        transform.rotation = endRotation;
        transform.position = endPosition;
        movementTargetPosition = transform.position;

        isFlipping = false;
    }
}
