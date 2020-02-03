using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ScoreType
{
    Bad = 1, 
    Nice = 2,
    Great = 4
}

[System.Serializable]
public class Score
{
    #region Properties
    public ScoreType Type => scoreType;
    public int ScoreTypeCount => scoreTypeCount;
    #endregion

    #region Serialized Private Members
    [Header("Score Properties")]
    [SerializeField] private ScoreType scoreType = ScoreType.Great;
    #endregion

    #region Private Members
    private int scoreTypeCount = 0;
    #endregion

    public void AddScore() => scoreTypeCount++;
    public void Reset() => scoreTypeCount = 0;
}

[System.Serializable]
public class ScoreEvent : UnityEvent<ScoreType> { }

public class ScoreHandler : MonoBehaviour
{
    #region Properties
    public int Score => currentScore;
    public int Multiplier => multiplier;
    public float ScorePercentage => (float)totalNotesHit / (float)noteCount;
    #endregion

    #region Serialized Private Members
    [SerializeField] private GameEvent onScoreUpdate = null;
    [SerializeField] private List<Score> scores = new List<Score>();

    [SerializeField] private int noteCount = 160;

    [SerializeField] private int currentScore = 0;
    [SerializeField] private int highScore = 0;

    [SerializeField] private int multiplier = 1;
    #endregion

    #region Private Members
    private int streak = 0;
    private int totalNotesHit = 0;
    #endregion

    private void OnValidate()
    {
        if (multiplier < 0)
        {
            multiplier = 1;
        }
    }

    public void AddScore(ScoreType type)
    {
        int scoreTypeMultiplier = (int)type;

        if (type == ScoreType.Bad)
            ResetMultiplierAndStreak();

        scores.Find(x => x.Type == type).AddScore();
        totalNotesHit++;
        currentScore += (20 * scoreTypeMultiplier) * multiplier;
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

    public int GetScoreTypeCount(ScoreType type)
    {
        return scores.Find(x => x.Type == type).ScoreTypeCount; 
    }

    public void ResetNoteHit()
    {
        totalNotesHit = 0;
    }
}
