using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region Singleton Code

    private GameManager instance = null;
    public GameManager Instance 
    {
        get 
        {
            if(!instance)
            {
                instance = FindObjectOfType<GameManager>();
                if(!instance)
                {
                    GameObject newInstance = new GameObject("Game Manager");
                    instance = newInstance.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    #endregion

    #region Properties
    public bool IsGameMuted => isGameMuted;
    #endregion

    #region Serialize Private Members
    [Header("Game Properties")]
    [SerializeField] private bool isGameMuted = false;
    [SerializeField] private bool hasGameStarted = false;

    [Header("Game Events")]
    [Space(10)]
    [SerializeField] private UnityEvent onGameStart = null;
    [SerializeField] private UnityEvent onGameEnd = null;
    #endregion

    private void Awake()
    {
        #region Singleton Code
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        #endregion
    }

    public void StartGame()
    {
        // Return if game has already started
        if (hasGameStarted)
        {
            return;
        }

        hasGameStarted = true;
        onGameStart.Invoke();
    }

    public void EndGame()
    {
        // Return if the game has not started
        if(!hasGameStarted)
        {
            return;
        }

        hasGameStarted = false;
        onGameEnd.Invoke();
    }

    public void ToggleMute()
    {
        isGameMuted = !isGameMuted;
    }
}
