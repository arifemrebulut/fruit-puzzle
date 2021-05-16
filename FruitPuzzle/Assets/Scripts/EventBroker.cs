using System;
using UnityEngine;

public class EventBroker
{
    public static Action OnJump;
    public static Action<Vector3> OnCantJump;
    public static Action OnFlipping;
    public static Action OnLevelComplete;

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

    public static void CallOnFlipping()
    {
        if (OnFlipping != null)
        {
            OnFlipping();
        }
    }

    public static void CallOnLevelComplete()
    {
        if (OnLevelComplete != null)
        {
            OnLevelComplete();
        }
    }
}
