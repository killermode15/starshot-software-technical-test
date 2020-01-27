using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class TouchHandler : MonoBehaviour
{
    [SerializeField] private GameEvent onLeftSideTouched = null;
    [SerializeField] private GameEvent onRightSideTouched = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
                Debug.Log("Right is pressed");
            }

        }
    }
}
