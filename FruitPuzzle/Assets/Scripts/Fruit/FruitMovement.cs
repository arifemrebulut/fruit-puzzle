using System.Collections;
using UnityEngine;

public class FruitMovement : MonoBehaviour
{
    [SerializeField] float movementDuration;
    [SerializeField] float flipSpeed;
    [SerializeField] float swipeDelta;

    private bool isTouching, isMoving, isFlipping, isCantJumping;

    private Vector3 touchStartPosition;

    FruitGridCheck fruitGridCheck;

    private void Start()
    {
        fruitGridCheck = GetComponent<FruitGridCheck>();
    }

    void Update()
    {
        if (!isTouching && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && !isFlipping && !isMoving && !isCantJumping)
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
                    isMoving = true;
                    StartCoroutine(MoveFruit(Vector3.forward));
                }
                else
                {
                    isTouching = false;
                    StartCoroutine(CantMove(Vector3.forward));
                }
            }
            else if (Input.GetTouch(0).position.y <= touchStartPosition.y - swipeDelta)
            {
                if (fruitGridCheck.CanMoveBack())
                {
                    isTouching = false;
                    isMoving = true;
                    StartCoroutine(MoveFruit(Vector3.back));
                }
                else
                {
                    isTouching = false;
                    StartCoroutine(CantMove(Vector3.back));
                }
            }
            else if (Input.GetTouch(0).position.x >= touchStartPosition.x + swipeDelta)
            {
                isTouching = false;
                if (fruitGridCheck.CanMoveRight())
                {
                    isFlipping = true;
                    StartCoroutine(FlipFruit(Vector3.right));
                }
                else
                {
                    StartCoroutine(CantMove(Vector3.right));
                }
            }
            else if (Input.GetTouch(0).position.x <= touchStartPosition.x - swipeDelta)
            {
                isTouching = false;
                if (fruitGridCheck.CanMoveLeft())
                {
                    isFlipping = true;
                    StartCoroutine(FlipFruit(Vector3.left));
                }
                else
                {
                    StartCoroutine(CantMove(Vector3.left));
                }
            }
        }
    }

    private IEnumerator CantMove(Vector3 direction)
    {
        isCantJumping = true;
        EventBroker.CallOnCantJump(direction);
        yield return new WaitForSecondsRealtime(0.5f);
        isCantJumping = false;
    }

    private IEnumerator MoveFruit(Vector3 direction)
    {
        float elapsedTime = 0f;
        Vector3 targetPosition = transform.position + direction;

        EventBroker.CallOnJump();

        while (elapsedTime < movementDuration)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;

        isMoving = false;
    }

    private IEnumerator FlipFruit(Vector3 flipDirection)
    {
        Vector3 rotAxis = Vector3.Cross(Vector3.up, flipDirection);
        Vector3 pivot = (transform.position + Vector3.down * 0.5f) + flipDirection * 0.5f;

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.AngleAxis(90.0f, rotAxis) * startRotation;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = transform.position + flipDirection;

        float rotSpeed = 90.0f / flipSpeed;
        float elapsedTime = 0f;

        EventBroker.CallOnFlipping();

        while (elapsedTime < flipSpeed)
        {
            elapsedTime += Time.deltaTime;
            transform.RotateAround(pivot, rotAxis, rotSpeed * Time.deltaTime);
            yield return null;
        }

        transform.rotation = endRotation;
        transform.position = endPosition;

        isFlipping = false;
    }
}
