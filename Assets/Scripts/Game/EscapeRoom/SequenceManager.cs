using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SequenceManager : MonoBehaviour
{

    [SerializeField] int sequenceDifficulty = 5;
    [SerializeField] float sequenceSpeed = 1f; 
    private int[] randomSequence;  
    private CubeButton[] cubes;
    public bool isSequencePlaying = false;

    public void SetupSequence(CubeButton[] cubes)
    {
        this.cubes = cubes;
        randomSequence = new int[sequenceDifficulty];
        GenerateRandomSequence();
    }

    public void GenerateRandomSequence()
    {
        int tempReference;
        for (int i = 0; i < sequenceDifficulty; i++)
        {
            tempReference = Random.Range(0, cubes.Length);
            randomSequence[i] = tempReference;
        }
    }

    public void PlaySequence()
    {
        StartCoroutine(PlaySequenceCoroutine());
    }

    IEnumerator PlaySequenceCoroutine()
    {
        isSequencePlaying = true; 
        foreach (int index in randomSequence)
        {
            cubes[index].Highlight();
            yield return new WaitForSeconds(sequenceSpeed);
        }
        isSequencePlaying = false;
    }

    public int GetCurrentSequenceStep(int stepIndex)
    {
        return randomSequence[stepIndex];
    }

    public int GetSequenceLength()
    {
        return randomSequence.Length;
    }
}

