using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIHandler : MonoBehaviour
{
    #region Serialized Private Members

    [Header("References")]
    [SerializeField] private PlayerHealth healthScript;
    [SerializeField] private Image healthUI;

    #endregion


    public void UpdateHealthUI()
    {
        healthUI.fillAmount = healthScript.HealthPercent;/* Mathf.Lerp(healthUI.fillAmount, healthScript.HealthPercent, Time.deltaTime * 10);*/
    }

}
