using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float swipeDelta;

    private Vector3 touchStartPosition;
    private Vector3 currentTouchPosition;

    public Vector3 swipeDirection { get; private set; }

    private void Update()
    {
        DetectSwipe();
    }

    public void DetectSwipe()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            swipeDirection = Vector3.zero;

            touchStartPosition = Input.mousePosition;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            currentTouchPosition = Input.GetTouch(0).position;

            if ((currentTouchPosition.y > touchStartPosition.y) && Mathf.Abs(currentTouchPosition.y - touchStartPosition.y) > swipeDelta)
            {
                swipeDirection = Vector3.forward;
                Debug.Log("Up");
            }
            else if ((currentTouchPosition.y < touchStartPosition.y) && Mathf.Abs(currentTouchPosition.y - touchStartPosition.y) > swipeDelta)
            {
                swipeDirection = Vector3.back;
                Debug.Log("Down");
            }
            else if ((currentTouchPosition.x > touchStartPosition.x) && Mathf.Abs(currentTouchPosition.x - touchStartPosition.x) > swipeDelta)
            {
                swipeDirection = Vector3.right;
                Debug.Log("Right");
            }
            else if ((currentTouchPosition.x < touchStartPosition.x) && Mathf.Abs(currentTouchPosition.x - touchStartPosition.x) > swipeDelta)
            {
                swipeDirection = Vector3.left;
                Debug.Log("Left");
            }
        }
    }
}
