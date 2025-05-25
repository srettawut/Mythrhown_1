using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notePrefab;  // Reference to the note prefab
    public AudioClip musicClip;    // The music clip to play
    private AudioSource audioSource;

    public float[] noteTimes ={ 1f, 2f, 3.5f, 5f, 6.2f, 8f };       // Array of times (in seconds) when notes should appear

    private int currentNoteIndex = 0;  // Index to track the current note time

    public float bpm = 175f;
    private float beatInterval;
    // Start is called before the first frame update
    void Start()
    {
        // Get the AudioSource and start playing the music
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = musicClip;
        audioSource.Play();

        beatInterval = 60f / bpm;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the current time in the music has passed the time for the next note
        if (currentNoteIndex < noteTimes.Length && audioSource.time >= noteTimes[currentNoteIndex])
        {
            SpawnNote();
            currentNoteIndex++;
        }
    }

    // Spawn a note at the appropriate position
    void SpawnNote()
    {
        float spawnX = Random.Range(-5f, 5f);  // Random x position for the note
        Vector3 spawnPosition = new Vector3(spawnX, Camera.main.orthographicSize, 0);  // Spawn at the top of the screen

        // Instantiate the note at the spawn position
        Instantiate(notePrefab, spawnPosition, Quaternion.identity);
    }
}
