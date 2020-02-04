using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    #region Properties
    public int NotesSpawned => noteCounter;
    #endregion

    #region Serialized Private Members

    [Header("Spawning Properties")]
    [SerializeField] private GameObject notePrefab = null;
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] private bool isSpawning = false;

    [Header("References")]
    [Space(10)]
    [SerializeField] private SongHandler songHandler = null;

    [Header("Note Properties")]
    [Space(10)]
    [SerializeField] private int startingNoteDensity = 1;
    [SerializeField] private float startingNoteSpeed = 1;
    #endregion

    #region Private Members
    private float songTempo = 0;
    private float timeFromLastSpawn = 0;
    private float spawnTimer = 0;

    private int noteDensity = 0;
    private float noteSpeed = 0;
    private int noteCounter = 0;

    private List<GameObject> notesSpawned = new List<GameObject>();
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        noteDensity = startingNoteDensity;
        noteSpeed = startingNoteSpeed;
    }

    private void OnValidate()
    {
        if (noteDensity <= 0)
            noteDensity = 1;
        if (noteSpeed <= 0)
            noteSpeed = 1;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!isSpawning)
            return;

        notesSpawned.RemoveAll(x => x == null);

        songTempo = songHandler.SongTempo / noteDensity;

        if (spawnTimer >= timeFromLastSpawn)
        {
            timeFromLastSpawn += songTempo;

            //Spawn note
            int randIndex = Random.Range(0, spawnPoints.Count);
            GameObject note = Instantiate(notePrefab, spawnPoints[randIndex]);
            notesSpawned.Add(note);
            note.transform.position = spawnPoints[randIndex].position;

            note.GetComponent<Note>().Speed = songHandler.SongTempo;
            noteCounter++;
        }
        else
        {
            spawnTimer += Time.deltaTime;
        }
    }

    public void StartSpawning()
    {
        isSpawning = true;
        timeFromLastSpawn = Time.time;
        spawnTimer = Time.time - songTempo;

        StartCoroutine(SpawningTimer_CR());
        StartCoroutine(DifficultyTimer_CR());

    }

    public void StopSpawning()
    {
        isSpawning = false;

        songTempo = 0;
        spawnTimer = 0;

        noteDensity = startingNoteDensity;
        noteSpeed = startingNoteSpeed;

        StopAllCoroutines();
    }

    public void CleanupNotes()
    {
        for (int i = notesSpawned.Count - 1; i >= 0; i--)
        {
            Destroy(notesSpawned[i]);
        }
    }

    private IEnumerator SpawningTimer_CR()
    {
        yield return new WaitForSeconds(songHandler.Clip.length - 3f);
        StopSpawning();
        Debug.Log("STOPPED SPAWNING");
    }


    private IEnumerator DifficultyTimer_CR()
    {
        float currentTimer = 0;
        float increment = 20; // Seconds

        while (isSpawning)
        {
            currentTimer += Time.deltaTime;

            if (currentTimer >= increment)
            {
                currentTimer = 0;
                if (noteDensity < 10)
                {
                    noteDensity += 2;
                }
                else if (noteSpeed < 6)
                {
                    noteSpeed += 1;
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(spawnPoint.position, 5);
        }
    }
#endif
}
