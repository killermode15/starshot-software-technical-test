using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUIHandler : MonoBehaviour
{
    [SerializeField] private ScoreHandler scoreHandler = null;
    [SerializeField] private TextMeshProUGUI scoreText = null;
    [SerializeField] private TextMeshProUGUI multiplierText = null;


    public void UpdateScoreAndMultiplier()
    {
        scoreText.text = "Score: " + scoreHandler.Score;
        multiplierText.text = "Multiplier: " + scoreHandler.Multiplier + "x";
    }
}
