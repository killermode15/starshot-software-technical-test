using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongHandler : MonoBehaviour
{
    public float SongTempo => tempo;

    [SerializeField] private bool hasSongStarted = false;

    [SerializeField] private AudioClip clip = null;
    [SerializeField] private float bpm = 0;

    private float tempo = 0;
    private AudioSource source = null;
    
    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = clip;
    }
    
    private void Update()
    {
        // Don't update when song has not started.
        if (!hasSongStarted)
        {
            return;
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
        tempo = bpm / 60;
    }

    public void StopSong()
    {
        if (!hasSongStarted)
        {
            return;
        }

        source.Stop();

        hasSongStarted = false;
        tempo = 0;
    }
}
