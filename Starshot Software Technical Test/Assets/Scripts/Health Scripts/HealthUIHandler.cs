using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIHandler : MonoBehaviour
{
    #region Serialized Private Members

    [Header("References")]
    [SerializeField] private PlayerHealth healthScript = null;
    [SerializeField] private Image healthUI = null;

    #endregion
    
    /// <summary>
    /// Updates the health UI bar
    /// </summary>
    public void UpdateHealthUI()
    {
        healthUI.fillAmount = healthScript.HealthPercent;
    }

}
