﻿using System;
using UnityEngine;

public class EventBroker
{
    public static Action OnJump;
    public static Action<Vector3> OnCantJump;
    public static Action OnFlipping;
    public static Action OnFruitFullCovered;
    public static Action OnLevelStart;
    public static Action OnFruitComplete;
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

    public static void CallOnFruitFullCovered()
    {
        if (OnFruitFullCovered != null)
        {
            OnFruitFullCovered();
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

    public static void CallOnLevelScene()
    {
        if (OnLevelPassed != null)
        {
            OnLevelPassed();
        }
    }
}
