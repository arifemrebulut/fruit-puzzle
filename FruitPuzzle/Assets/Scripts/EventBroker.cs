using System;
using UnityEngine;

public class EventBroker
{
    public static Action OnJump;
    public static Action<Vector3> OnCantJump;
    public static Action OnFlipping;
    public static Action OnLevelStart;
    public static Action OnFruitRiseEnd;
    public static Action OnFruitComplete;
    public static Action OnFinishedFruitScene;
    public static Action OnLevelPassed;

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

    public static void CallOnLevelStart()
    {
        if (OnLevelStart != null)
        {
            OnLevelStart();
        }
    }

    public static void CallOnFruitComplete()
    {
        if (OnFruitComplete != null)
        {
            OnFruitComplete();
        }
    }

    public static void CallOnFruitRiseEnd()
    {
        if (OnFruitRiseEnd != null)
        {
            OnFruitRiseEnd();
        }
    }

    public static void CallOnFinishedFruitScene()
    {
        if (OnFinishedFruitScene != null)
        {
            OnFinishedFruitScene();
        }
    }

    public static void CallOnLevelPassed()
    {
        if (OnLevelPassed != null)
        {
            OnLevelPassed();
        }
    }
}
