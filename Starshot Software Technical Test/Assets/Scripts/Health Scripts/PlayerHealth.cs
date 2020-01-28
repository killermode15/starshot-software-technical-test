using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    #region Properties
    public float CurrentHealth => currentHealth;
    public float HealthPercent => (float) currentHealth / (float) maxHealth;
    #endregion

    #region Serialized Private Members
    [Header("Health Properties")]
    [SerializeField] private int maxHealth = 100;

    [Header("Health Events")]
    [Space(10)]
    [SerializeField] private GameEvent onHealthUpdate;
    [SerializeField] private GameEvent onHealthFinished;
    #endregion

    #region Private Members
    private int currentHealth = 0;
    #endregion

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public void ReduceHealth(int amount = 10)
    {
        if (amount <= 0)
            return;

        currentHealth -= amount;
        onHealthUpdate.Raise();
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            onHealthFinished.Raise();
            Debug.Log("Test");
        }
    }

    public void AddHealth(int amount = 5)
    {
        if (amount <= 0)
            return;

        currentHealth += amount;
        onHealthUpdate.Raise();
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

}
