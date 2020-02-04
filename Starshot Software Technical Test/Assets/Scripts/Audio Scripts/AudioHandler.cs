using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    #region Serialized Private Members
    [Header("References")]
    [SerializeField] private AudioManager audioManager = null;
    #endregion

    /// <summary>
    /// Plays a sound effect from the audio manager.
    /// </summary>
    /// <param name="identifier">An audio data's string id</param>
    public void PlaySFX(string identifier)
    {
        AudioSource source = audioManager.GetAudio(identifier).PlayAudio(destroyAfter: true);
    }
}