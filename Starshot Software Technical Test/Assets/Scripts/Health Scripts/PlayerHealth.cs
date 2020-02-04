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
    [SerializeField] private GameEvent onHealthUpdate = null;
    [SerializeField] private GameEvent onHealthFinished = null;
    #endregion

    #region Private Members
    private int currentHealth = 0;
    #endregion

    /// <summary>
    /// Resets the current health to the max
    /// </summary>
    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Reduces health by an amount
    /// </summary>
    /// <param name="amount">Amount to reduce from current health. Set to 10 by default.</param>
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
        }
    }

    /// <summary>
    /// Adds health by an amount
    /// </summary>
    /// <param name="amount">Amount to increase to current health. Set to 5 by default</param>
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
