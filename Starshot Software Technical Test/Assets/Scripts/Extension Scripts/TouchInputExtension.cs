using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TouchInputExtension 
{
    public static bool OnTouchDown(this Touch touch)
    {
        return touch.phase == TouchPhase.Began;
    }

    public static bool OnTouchUp(this Touch touch)
    {
        return touch.phase == TouchPhase.Ended;
    }

    public static bool OnTouchDrag(this Touch touch)
    {
        return touch.phase == TouchPhase.Moved;
    }

    public static bool OnTouchStatic(this Touch touch)
    {
        return touch.phase == TouchPhase.Stationary;
    }
}
