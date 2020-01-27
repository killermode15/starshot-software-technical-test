using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteCatcher : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Note"))
            return;

        other.GetComponent<Note>().NoteMissed();
    }
}
