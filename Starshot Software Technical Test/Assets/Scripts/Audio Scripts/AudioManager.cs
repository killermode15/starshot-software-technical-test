using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class AudioData
{
    #region Properties
    public string Identifier => identifier;

    public float Volume => volume;
    public bool UseSnapshot => useSnapshot;

    public AudioClip AudioClip => audioClip;
    public AudioMixerSnapshot MixerSnapshot => mixerSnapshot;
    #endregion


    #region Serialized Private Members
    [Header("Audio Data Properties")]
    [SerializeField] private string identifier = string.Empty;
    [SerializeField, Range(0, 1)] private float volume = 0.75f;
    [SerializeField] private bool useSnapshot = false;

    [Header("Audio Data References")]
    [SerializeField] private AudioClip audioClip = null;
    [SerializeField] private AudioMixerSnapshot mixerSnapshot = null;
    #endregion


    /// <summary>
    /// Returns an audio source with the audio clip attached.
    /// </summary>
    /// <param name="play">Automatically play the audio source if true</param>
    /// <param name="destroyAfter">Destroys the audio source after audio is played if true</param>
    /// <returns></returns>
    public AudioSource PlayAudio(bool play = true, bool destroyAfter = false)
    {
        if (!UseSnapshot)
        {
            AudioSource source = new GameObject("Source").AddComponent<AudioSource>();
            source.clip = AudioClip;
            source.volume = volume;
            if (play)
            {
                source.Play();
            }
            if (destroyAfter)
            {
                GameObject.Destroy(source.gameObject, source.clip.length);
            }
            return source;
        }
        else
        {
            Debug.LogWarning("Using PlayAudio() while Use Snapshot is true");
            return null;
        }
    }

    /// <summary>
    /// Plays an audio mixer snapshot
    /// </summary>
    /// <param name="timeToReach">Transition time between the current snapshot to this</param>
    public void PlaySnapshot(float timeToReach)
    {
        if (UseSnapshot)
        {
            MixerSnapshot.TransitionTo(timeToReach);
        }
        else
        {
            Debug.LogWarning("Using PlaySnapshot() while Use Snapshot is false");
        }
    }
}

[CreateAssetMenu(fileName = "Audio Manager")]
public class AudioManager : ScriptableObject
{
    private List<AudioData> AudioData;

    /// <summary>
    /// Returns an audio data with the corresponding identifier
    /// </summary>
    /// <param name="identifier">The audio data's string id</param>
    /// <returns></returns>
    public AudioData GetAudio(string identifier)
    {
        return AudioData.Find(x => x.Identifier.ToLower() == identifier.ToLower());
    }
}