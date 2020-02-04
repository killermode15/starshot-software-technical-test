using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenHandler : MonoBehaviour
{
    #region Serialized Private Members
    [Header("References")]
    [SerializeField] private GameScreen mainScreen = null;
    [SerializeField] private List<GameScreen> screens = new List<GameScreen>();
    #endregion

    #region Private Members
    private GameScreen activeScreen = null;
    #endregion
    
    private void Start()
    {
        foreach (GameScreen screen in screens)
        {
            if (screen)
            {
                screen.ToggleScreen(false);
            }
        }

        mainScreen.ToggleScreen(true);
        activeScreen = mainScreen;
    }

    /// <summary>
    /// Switches between the current screen to a new screen
    /// </summary>
    /// <param name="screen">The screen to switch to</param>
    public void SwitchScreen(GameScreen screen)
    {
        activeScreen.ToggleScreen(false);
        activeScreen = screen;
        activeScreen.ToggleScreen(true);
    }
}
