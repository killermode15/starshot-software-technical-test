using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuUIHandler : MonoBehaviour
{
    #region Serialized Private Members
    [Header("References")]
    [SerializeField] private GameManager gameManager = null;
    [SerializeField] private TextMeshProUGUI muteButtonText = null;
    #endregion
    public void UpdateMenuUI()
    {
        muteButtonText.text = gameManager.IsGameMuted ? "Unmute" : "Mute";
    }
}