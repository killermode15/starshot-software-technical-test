using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    #region Serialized Private Members
    [Header("References")]
    [SerializeField] private AudioManager audioManager = null;
    #endregion

    public void PlaySFX(string identifier)
    {
        AudioSource source = audioManager.GetAudio(identifier).PlayAudio(destroyAfter: true);
    }
}