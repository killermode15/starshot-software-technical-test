using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ScoreType
{
    Miss = 0,
    Bad = 1,
    Nice = 2,
    Great = 4,
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
    [Header("Score Properties")]
    [SerializeField] private int currentScore = 0;
    [SerializeField] private int multiplier = 1;
    [SerializeField] private int noteCount = 160;
    [SerializeField] private List<Score> scores = new List<Score>();

    [Header("Score Events")]
    [Space(10)]
    [SerializeField] private GameEvent onScoreUpdate = null;
    #endregion

    #region Private Members
    private int streak = 0;
    private int totalNotesHit = 0;
    #endregion

    /// <summary>
    /// Makes sure that the multiplier doesn't go below 0.
    /// </summary>
    private void OnValidate()
    {
        if (multiplier < 0)
        {
            multiplier = 1;
        }
    }

    /// <summary>
    /// Adds a score based on the given score type.
    /// </summary>
    /// <param name="type"></param>
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

    /// <summary>
    /// Increases the multiplier
    /// </summary>
    // Called whenever a certain streak is reached
    public void AddMultiplier()
    {
        if (multiplier >= 8)
            return;
        multiplier *= 2;
    }

    /// <summary>
    /// Resets the current multiplier and streak
    /// </summary>
    // Called when a NoteMissed event is raise
    public void ResetMultiplierAndStreak()
    {
        multiplier = 1;
        streak = 0;
        onScoreUpdate.Raise();
    }

    /// <summary>
    /// Add a miss to the counter
    /// </summary>
    public void AddMiss()
    {
        scores.Find(x => x.Type == ScoreType.Miss).AddScore();
    }

    /// <summary>
    /// Resets all the scores
    /// </summary>
    public void ResetScore()
    {
        currentScore = 0;
        totalNotesHit = 0;
        foreach (Score score in scores)
        {
            score.Reset();
        }
        ResetMultiplierAndStreak();
    }

    /// <summary>
    /// Adds a streak when the NoteCaught event is raised
    /// </summary>
    public void AddStreak()
    {
        streak++;

        if (streak % (10 * multiplier / 2) == 0)
        {
            AddMultiplier();
        }
    }

    /// <summary>
    ///  Gets the count of a score type
    /// </summary>
    /// <param name="type"></param>
    /// <returns>Returns the count of the provided score type</returns>
    public int GetScoreTypeCount(ScoreType type)
    {
        return scores.Find(x => x.Type == type).ScoreTypeCount;
    }
}
