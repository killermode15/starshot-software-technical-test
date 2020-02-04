using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NoteCatcher : MonoBehaviour
{
    #region Serialized Private Members
    [Header("Note Events")]
    [SerializeField] private ScoreEvent onNoteMissed = null;
    #endregion

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Note"))
            return;

        onNoteMissed.Invoke(ScoreType.Miss);
        other.GetComponent<Note>().NoteMissed();
    }
}
