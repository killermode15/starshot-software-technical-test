using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{

    public int Score => currentScore;
    public int Multiplier => multiplier;

    [SerializeField] private GameEvent onScoreUpdate;

    [SerializeField] private int currentScore = 0;
    [SerializeField] private int highScore = 0;

    [SerializeField] private int multiplier = 1;

    private int streak = 0;

    private void OnValidate()
    {
        if (multiplier < 0)
        {
            multiplier = 1;
        }
    }

    public void AddScore()
    {
        currentScore += 20 * multiplier;
        onScoreUpdate.Raise();
    }

    // Called whenever a certain streak is reached
    public void AddMultiplier()
    {
        if (multiplier >= 8)
            return;
        multiplier *= 2;

    }

    // Called when a NoteMissed event is raise
    public void ResetMultiplierAndStreak()
    {
        multiplier = 1;
        streak = 0;
        onScoreUpdate.Raise();
    }

    // Called when a NoteCaught event is raised
    public void AddStreak()
    {
        streak++;

        if(streak % (10 * multiplier/2) == 0)
        {
            AddMultiplier();
        }
    }

    private void SaveScore()
    {
        if(highScore < currentScore)
        {
            highScore = currentScore;
        }

        PlayerPrefs.SetInt("playerScore", currentScore);
        PlayerPrefs.SetInt("highScore", highScore);
    }

}
