using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenHandler : MonoBehaviour
{
    [SerializeField] private GameScreen mainScreen = null;
    [SerializeField] private List<GameScreen> screens = new List<GameScreen>();

    private GameScreen activeScreen = null;

    // Start is called before the first frame update
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

    // Update is called once per frame
    private void Update()
    {

    }

    public void SwitchScreen(GameScreen screen)
    {
        activeScreen.ToggleScreen(false);
        activeScreen = screen;
        activeScreen.ToggleScreen(true);
    }
}
