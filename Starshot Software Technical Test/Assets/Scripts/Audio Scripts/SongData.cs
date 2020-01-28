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
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float audioBPM;
    #endregion
}
