using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuUIHandler : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private TextMeshProUGUI muteButtonText;

    public void UpdateMenuUI()
    {
        muteButtonText.text = gameManager.IsGameMuted ? "Unmute" : "Mute";
    }
}