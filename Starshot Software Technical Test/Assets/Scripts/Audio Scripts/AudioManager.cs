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
    [SerializeField] private string identifier;
    [SerializeField, Range(0, 1)] private float volume;
    [SerializeField] private bool useSnapshot = false;

    [Header("Audio Data References")]
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioMixerSnapshot mixerSnapshot;
    #endregion

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
    public List<AudioData> AudioData;

    public AudioData GetAudio(string identifier)
    {
        return AudioData.Find(x => x.Identifier.ToLower() == identifier.ToLower());
    }
}