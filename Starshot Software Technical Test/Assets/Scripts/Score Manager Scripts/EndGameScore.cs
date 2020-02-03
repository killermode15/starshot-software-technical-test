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
    [Header("References")]
    [SerializeField] private ScoreHandler scoreHandler = null;

    [Header("UI References")]
    [Space(10)]
    [SerializeField] private TextMeshProUGUI ratingText = null;
    [SerializeField] private TextMeshProUGUI badScoreCount = null;
    [SerializeField] private TextMeshProUGUI niceScoreCount = null;
    [SerializeField] private TextMeshProUGUI greatScoreCount = null;
    [SerializeField] private TextMeshProUGUI totalScore = null;

    [Header("Score Rating Reference")]
    [Space(10)]
    [SerializeField] private List<RatingScore> ratings = new List<RatingScore>();
    #endregion

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
        badScoreCount.text = scoreHandler.GetScoreTypeCount(ScoreType.Bad).ToString();
        niceScoreCount.text = scoreHandler.GetScoreTypeCount(ScoreType.Nice).ToString();
        greatScoreCount.text = scoreHandler.GetScoreTypeCount(ScoreType.Great).ToString();
        totalScore.text = scoreHandler.Score.ToString();
    }

    public void Share()
    {
        StartCoroutine(TakeScreenshot());
    }

    private IEnumerator TakeScreenshot()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();
        
        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // To avoid memory leaks
        Destroy(ss);

        new NativeShare().AddFile(filePath).SetText("Hello world!").Share();
    }
}
