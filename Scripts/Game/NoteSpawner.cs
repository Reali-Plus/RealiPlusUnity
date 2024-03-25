using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notePrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 1f;

    private List<string> notes = new List<string>();
    private int currentNoteIndex = 0;
    private float nextSpawnTime;

    void Start()
    {
        notes.AddRange(new string[] {
            "C4", "C4", "G4", "G4", "A4", "A4", "G4", "F4", "F4", "E4", "E4", "D4", "D4", "C4",
            "G4", "G4", "F4", "F4", "E4", "E4", "D4", "G4", "G4", "F4", "F4", "E4", "E4", "D4", "C4",
            "G4", "G4", "A4", "G4", "F4", "F4", "E4", "E4", "D4", "C4"
        });

        nextSpawnTime = Time.time;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime && currentNoteIndex < notes.Count)
        {
            SpawnNote();
            nextSpawnTime += spawnInterval;
            currentNoteIndex++;
        }
    }

    void SpawnNote()
    {
        string currentNote = notes[currentNoteIndex];

        Transform spawnPoint = GetSpawnPoint(currentNote);

        if (spawnPoint != null)
        {
            GameObject note = Instantiate(notePrefab, spawnPoint.position, Quaternion.identity);

            note.GetComponent<Note>().setNoteText(currentNote);
        }
        else
        {
            Debug.LogWarning("No spawn point found for note: " + currentNote);
        }
    }

    Transform GetSpawnPoint(string note)
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            if (spawnPoint.name == note)
            {
                return spawnPoint;
            }
        }

        return null;
    }
}
