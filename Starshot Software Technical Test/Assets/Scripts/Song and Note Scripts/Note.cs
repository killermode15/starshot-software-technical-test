using EventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float Speed {
        get 
        {
            return speed;
        }
        set 
        {
            speed = value;
        }

    }

    [SerializeField] private GameEvent onNoteCaught = null;
    [SerializeField] private GameEvent onNoteMissed = null;

    private float speed = 0;

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position -= new Vector3(0, Speed, 0);
    }

    public void NoteCaught()
    {
        onNoteCaught.Raise();
        Destroy(gameObject);
    }

    public void NoteMissed()
    {
        onNoteMissed.Raise();
        Destroy(gameObject);
    }
    
}
