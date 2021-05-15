using UnityEngine;

public class FruitGridCheck : MonoBehaviour
{
    [SerializeField] LayerMask avaibleGridLayers;
    [SerializeField] float raycastLength;

    Vector3 forwardOrigin, backOrigin, leftOrigin, rightOrigin;

    private void Update()
    {
        forwardOrigin = transform.position + new Vector3(0f, -0.4f, 0.5f);
        backOrigin = transform.position + new Vector3(0f, -0.4f, -0.5f);
        leftOrigin = transform.position + new Vector3(-0.5f, -0.4f, 0f);
        rightOrigin = transform.position + new Vector3(0.5f, -0.4f, 0f);
    }

    public bool CanMoveForward()
    {
        bool canMoveForward;

        canMoveForward = Physics.Raycast(forwardOrigin, Vector3.forward, raycastLength, avaibleGridLayers);

        return canMoveForward;
    }

    public bool CanMoveBack()
    {
        bool canMoveBack;

        canMoveBack = Physics.Raycast(backOrigin, Vector3.back, raycastLength, avaibleGridLayers);

        return canMoveBack;
    }

    public bool CanMoveLeft()
    {
        bool canMoveLeft;

        canMoveLeft = Physics.Raycast(leftOrigin, Vector3.left, raycastLength, avaibleGridLayers);

        return canMoveLeft;
    }

    public bool CanMoveRight()
    {
        bool canMoveRight;

        canMoveRight = Physics.Raycast(rightOrigin, Vector3.right, raycastLength, avaibleGridLayers);

        return canMoveRight;
    }
}
