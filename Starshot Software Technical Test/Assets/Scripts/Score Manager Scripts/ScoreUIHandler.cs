using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class ScoreTypeColor
{
    #region Properties
    public ScoreType Type => scoreType;
    public Color TextColor => textColor;
    #endregion

    #region Serialized Private Members
    [Header("Score Type Properties")]
    [SerializeField] private ScoreType scoreType = ScoreType.Great;
    [SerializeField] private Color textColor = Color.white;
    #endregion
}

public class ScoreUIHandler : MonoBehaviour
{
    #region Serialized Private Members
    [Header("References")]
    [SerializeField] private ScoreHandler scoreHandler = null;
    [Space(5)]
    [SerializeField] private TextMeshProUGUI scoreText = null;
    [SerializeField] private TextMeshProUGUI multiplierText = null;
    [SerializeField] private TextMeshProUGUI scoreTypeText = null;
    [Space(5)]
    [SerializeField] private Animator scoreTypeAnimator = null;


    [Header("Score UI Properties")]
    [Space(10)]
    [SerializeField] private List<ScoreTypeColor> scoreTypeColors = new List<ScoreTypeColor>();
    #endregion


    /// <summary>
    /// Updates the score and multiplier UI
    /// </summary>
    public void UpdateScoreAndMultiplier()
    {
        scoreText.text = "Score: " + scoreHandler.Score;
        multiplierText.text = "Multiplier: " + scoreHandler.Multiplier + "x";
    }

    /// <summary>
    /// Show the score type when a note is caught
    /// </summary>
    /// <param name="scoreType"></param>
    public void ShowScoreType(ScoreType scoreType)
    {
        scoreTypeAnimator.SetTrigger("Activate");
        scoreTypeText.text = scoreType.ToString();

        scoreTypeText.color = scoreTypeColors.Find(x => x.Type == scoreType).TextColor;
    }
}
