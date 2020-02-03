using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchButton : MonoBehaviour
{
    #region Serialized Private Members
    [Header("Button Events")]
    [SerializeField] private ScoreEvent onButtonTouched = null;
    #endregion

    #region Private Members
    private GameObject currentNote = null;
    #endregion

    // Checks if a note is on the circle
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Note"))
        {
            return;
        }

        currentNote = other.gameObject;
    }

    // Checks if a note left the circle
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Note"))
        {
            return;
        }

        currentNote = null;
    }

    // This is called when the button corresponding to the side of the screen is pressed
    public void OnCirclePressed()
    { 
        if (!currentNote)
        {
            return;
        }

        float dist = Vector3.Distance(transform.position, currentNote.transform.position);
        ScoreType scoreType = ScoreType.Great;

        if(dist >= 6)
        {
            scoreType = ScoreType.Bad;
        }
        else if(dist < 6 && dist >= 4f)
        {
            scoreType = ScoreType.Nice;
        }
        onButtonTouched.Invoke(scoreType);
        currentNote.GetComponent<Note>().NoteCaught();
    }
}
