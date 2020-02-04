using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TouchInputExtension 
{
    /// <summary>
    /// Returns true if a detected touch is tapped
    /// </summary>
    /// <param name="touch"></param>
    /// <returns></returns>
    public static bool OnTouchDown(this Touch touch)
    {
        return touch.phase == TouchPhase.Began;
    }

    /// <summary>
    /// Returns true if a detected touch has been lifted
    /// </summary>
    /// <param name="touch"></param>
    /// <returns></returns>
    public static bool OnTouchUp(this Touch touch)
    {
        return touch.phase == TouchPhase.Ended;
    }

    /// <summary>
    /// Returns true if a detected touch is dragged
    /// </summary>
    /// <param name="touch"></param>
    /// <returns></returns>
    public static bool OnTouchDrag(this Touch touch)
    {
        return touch.phase == TouchPhase.Moved;
    }

    /// <summary>
    /// Returns true if a detected touch is not moving
    /// </summary>
    /// <param name="touch"></param>
    /// <returns></returns>
    public static bool OnTouchStatic(this Touch touch)
    {
        return touch.phase == TouchPhase.Stationary;
    }
}
