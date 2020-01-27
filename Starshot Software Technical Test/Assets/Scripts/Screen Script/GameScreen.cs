using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ScreenType
{
    Null = -1,
    MainMenu = 0,
    Game = 1,
    EndMenu = 2
}

public class GameScreen : MonoBehaviour
{
    public ScreenType ScreenType => screenType;
    public bool IsScreenActive => isScreenActive;

    [Header("Screen Properties")]
    [SerializeField] private ScreenType screenType = ScreenType.Null;
    [SerializeField] private bool isScreenActive = false;

    [Header("Screen Events")]
    [Space(10)]
    [SerializeField] private UnityEvent onScreenActivate = new UnityEvent();
    [SerializeField] private UnityEvent onScreenDeactivate = new UnityEvent();

    // Start is called before the first frame update
    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleScreen(bool toggleVal)
    {
        isScreenActive = toggleVal;
        gameObject.SetActive(isScreenActive);

        if(isScreenActive)
        {
            onScreenActivate.Invoke();
        }
        else
        {
            onScreenDeactivate.Invoke();
        }
    }

    //public void ActivateScreen()
    //{

    //}

    //public void DeactiveScreen()
    //{

    //}
}
