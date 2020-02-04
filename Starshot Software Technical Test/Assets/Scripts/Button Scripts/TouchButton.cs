using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TouchButton : MonoBehaviour
{
    #region Serialized Private Members
    [Header("References")]
    [SerializeField] private Animation vignetteAnimation = null;

    [Header("Button Events")]
    [Space(10)]
    [SerializeField] private ScoreEvent onButtonTouched = null;
    #endregion

    #region Private Members
    private GameObject currentNote = null;
    private Queue<GameObject> notesDetected = new Queue<GameObject>();
    
    #endregion

    private void FixedUpdate()
    {
        List<GameObject> notes = notesDetected.ToList();
        notes.RemoveAll(x => x == null);
        notesDetected = new Queue<GameObject>(notes);
    }

    // Checks if a note is on the circle
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Note"))
        {
            return;
        }

        //currentNote = other.gameObject;
        notesDetected.Enqueue(other.gameObject);

    }

    // Checks if a note is out of the circle
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Note"))
        {
            return;
        }

        //currentNote = other.gameObject;
        if (notesDetected.Count > 0)
            notesDetected.Dequeue();

    }

    // This is called when the button corresponding to the side of the screen is pressed
    public void OnCirclePressed()
    {
        vignetteAnimation.Play("Vignette");

        if (notesDetected.Count <= 0)
        {
            return;
        }

        currentNote = notesDetected.Dequeue();
        if (!currentNote)
            return;

        float dist = Vector3.Distance(transform.position, currentNote.transform.position);
        ScoreType scoreType = ScoreType.Great;

        if (dist >= 6)
        {
            scoreType = ScoreType.Bad;
        }
        else if (dist < 6 && dist >= 4f)
        {
            scoreType = ScoreType.Nice;
        }

        onButtonTouched.Invoke(scoreType);
        currentNote.GetComponent<Note>().NoteCaught();
        currentNote = null;
    }
}
