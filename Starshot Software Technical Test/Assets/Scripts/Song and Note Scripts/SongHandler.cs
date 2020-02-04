using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongHandler : MonoBehaviour
{
    #region Properties
    public float SongTempo => tempo;
    public AudioClip Clip => songData.Clip;
    #endregion

    #region Serialized Private Members
    [Header("Song Properties")]
    [SerializeField] private SongData songData = null;
    [SerializeField] private bool hasSongStarted = false;

    [Header("References")]
    [Space(10)]
    [SerializeField] private GameManager gameManager = null;

    [Header("Song Events")]
    [Space(10)]
    [SerializeField] private GameEvent onSongEnd = null;
    #endregion

    #region Private Members
    private float tempo = 0;
    private AudioSource source = null;
    #endregion

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        source.clip = songData.Clip;
    }
    
    private void Update()
    {
        // Don't update when song has not started.
        if (!hasSongStarted)
        {
            return;
        }

        if (source.mute != gameManager.IsGameMuted)
        {
            source.mute = gameManager.IsGameMuted;
        }

        if(!source.isPlaying)
        {
            StopSong();
        }
    }

    // Starts the song if it has not started and sets the tempo of the song.
    public void StartSong()
    {
        if(hasSongStarted)
        {
            return;
        }

        source.Play();

        hasSongStarted = true;
        tempo = songData.BPM / 60;
    }

    public void StopSong()
    {
        if (!hasSongStarted)
        {
            return;
        }

        source.Stop();
        onSongEnd.Raise();

        hasSongStarted = false;
        tempo = 0;
    }
}
