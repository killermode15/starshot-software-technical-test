using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using UnityEngine.Events;
using UnityEngine.UIElements;


public class TouchHandler : MonoBehaviour
{
    #region Serialized Private Properties
    [Header("Touch Events")]
    [SerializeField] private GameEvent onLeftSideTouched = null;
    [SerializeField] private GameEvent onRightSideTouched = null;
    #endregion
    
    // Update is called once per frame
    void Update()
    {
        // Get all the touch inputs
        Touch[] touches = Input.touches;

        // Return if there are no touches detected
        if (touches.Length <= 0)
            return;

        foreach (Touch touch in touches)
        {
            if (!touch.OnTouchDown())
            {
                return;
            }

            float halfScreen = Screen.width/2;
            if(touch.position.x <= halfScreen)
            {
                onLeftSideTouched.Raise();
            }
            else
            {
                onRightSideTouched.Raise();
            }
        }
    }
}
