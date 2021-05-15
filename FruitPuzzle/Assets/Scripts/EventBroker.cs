using System;
using UnityEngine;

public class EventBroker : MonoBehaviour
{
    public static Action OnJump;
    public static Action<Vector3> OnCantJump;

    public static void CallOnJump()
    {
        if (OnJump != null)
        {
            OnJump();
        }
    }

    public static void CallOnCantJump(Vector3 direction)
    {
        if (OnCantJump != null)
        {
            OnCantJump(direction);
        }
    }
}
