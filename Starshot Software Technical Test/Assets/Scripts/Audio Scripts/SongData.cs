using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SongData : ScriptableObject
{
    #region Properties
    public AudioClip Clip => audioClip;
    public float BPM => audioBPM;
    #endregion

    #region Serialized Private Members
    [Header("Song Properties")]
    [SerializeField] private AudioClip audioClip = null;
    [SerializeField] private float audioBPM = 0;
    #endregion
}
