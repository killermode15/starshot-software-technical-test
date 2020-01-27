using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchButton : MonoBehaviour
{
    private GameObject currentNote = null;

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
        Debug.Log("test");
        if (!currentNote)
        {
            return;
        }

        currentNote.GetComponent<Note>().NoteCaught();
    }
}
