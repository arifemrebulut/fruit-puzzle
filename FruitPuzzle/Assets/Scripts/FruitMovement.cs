using System.Collections;
using UnityEngine;

public class FruitMovement : MonoBehaviour
{
    [SerializeField] float gridMovementDuration;
    [SerializeField] float swipeDelta;

    private PlayerInput playerInput;

    private bool isGridMoving;

    private Vector3 touchStartPosition;
    private Vector3 currentTouchPosition;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touchStartPosition = Input.mousePosition;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            currentTouchPosition = Input.GetTouch(0).position;

            if ((currentTouchPosition.y > touchStartPosition.y) && Mathf.Abs(currentTouchPosition.y - touchStartPosition.y) > swipeDelta)
            {
                StartCoroutine(GridMovement(Vector3.forward));
                Debug.Log("Up");
            }
            else if ((currentTouchPosition.y < touchStartPosition.y) && Mathf.Abs(currentTouchPosition.y - touchStartPosition.y) > swipeDelta)
            {
                StartCoroutine(GridMovement(Vector3.back));
                Debug.Log("Down");
            }
            //else if ((currentTouchPosition.x > touchStartPosition.x) && Mathf.Abs(currentTouchPosition.x - touchStartPosition.x) > swipeDelta)
            //{
            //    swipeDirection = Vector3.right;
            //    Debug.Log("Right");
            //}
            //else if ((currentTouchPosition.x < touchStartPosition.x) && Mathf.Abs(currentTouchPosition.x - touchStartPosition.x) > swipeDelta)
            //{
            //    swipeDirection = Vector3.left;
            //    Debug.Log("Left");
            //}
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
