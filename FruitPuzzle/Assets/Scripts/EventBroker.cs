using System;
using UnityEngine;

public class EventBroker : MonoBehaviour
{
    public static Action OnMove;
    public static Action OnCantMove;

    public static void CallOnMove()
    {
        OnMove();
    }

    public static void CallOnCantMove()
    {
        OnCantMove();
    }
}
