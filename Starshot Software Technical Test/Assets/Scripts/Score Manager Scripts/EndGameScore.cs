using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public enum RatingLetter
{
    C, // below 60%
    B, // 60%
    A, // 70%
    S, // 90%
    SS, // 100%
}


[System.Serializable]
public class RatingScore
{
    #region Properties
    public float RequiredPercent => requiredPercent;
    public string LetterGrade => rating.ToString();
    #endregion

    #region Serialized Private Members
    [Header("Rating Score Properties")]
    [SerializeField, Range(0, 1)] private float requiredPercent = 0;
    [SerializeField] private RatingLetter rating = RatingLetter.C;
    #endregion
}

public class EndGameScore : MonoBehaviour
{
    #region Serialized Private Members
    [Header("Score References")]
    [SerializeField] private ScoreHandler scoreHandler = null;

    [Header("UI References")]
    [Space(10)]
    [SerializeField] private TextMeshProUGUI ratingText = null;
    [SerializeField] private TextMeshProUGUI missScoreCount = null;
    [SerializeField] private TextMeshProUGUI badScoreCount = null;
    [SerializeField] private TextMeshProUGUI niceScoreCount = null;
    [SerializeField] private TextMeshProUGUI greatScoreCount = null;
    [SerializeField] private TextMeshProUGUI totalScore = null;

    [Header("Score Rating Reference")]
    [Space(10)]
    [SerializeField] private List<RatingScore> ratings = new List<RatingScore>();
    #endregion

    /// <summary>
    /// Updates the end screen score and rating
    /// </summary>
    public void UpdateEndScreen()
    {
        string ratingLetter = string.Empty;
        foreach (RatingScore rating in ratings)
        {
            if(rating.RequiredPercent <= scoreHandler.ScorePercentage)
            {
                ratingLetter = rating.LetterGrade;
            }
        }

        ratingText.text = ratingLetter;
        missScoreCount.text = scoreHandler.GetScoreTypeCount(ScoreType.Miss).ToString();
        badScoreCount.text = scoreHandler.GetScoreTypeCount(ScoreType.Bad).ToString();
        niceScoreCount.text = scoreHandler.GetScoreTypeCount(ScoreType.Nice).ToString();
        greatScoreCount.text = scoreHandler.GetScoreTypeCount(ScoreType.Great).ToString();
        totalScore.text = scoreHandler.Score.ToString();
    }
    
    /// <summary>
    /// Starts the screenshot task.
    /// </summary>
    public void Share()
    {
        StartCoroutine(TakeScreenshotTask());
    }

    /// <summary>
    /// Creates a Texture2D and saves the screen's current state
    /// and is shared using the phone's native sharing.
    /// </summary>
    /// <returns></returns>
    private IEnumerator TakeScreenshotTask()
    {
        // Wait for graphics to render
        yield return new WaitForEndOfFrame();

        // Save the graphics to a Texture2D
        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();
        
        // Save the image in a temporary cache
        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // To avoid memory leaks
        Destroy(ss);

        // Share the image through native sharing
        new NativeShare().AddFile(filePath).SetText("I scored " + scoreHandler.Score + "!").Share();
    }
}
