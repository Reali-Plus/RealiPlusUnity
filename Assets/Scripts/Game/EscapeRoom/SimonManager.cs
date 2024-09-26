using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonManager : MonoBehaviour
{
    [SerializeField] CubeButton[] cubes; // Tableau pour les cubes
    [SerializeField] int sequenceDifficulty = 5; //difficulte
    [SerializeField] float sequenceSpeed = 1f; //vitesse
    [SerializeField] private int[] randomSequence;  //sequence aleatoire
    private List<int> userSequence = new List<int>();  // Sequence entrée par l'utilisateur
    private int currentStep = 0;

    private void Start()
    {
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
        foreach (int index in randomSequence)
        {
            cubes[index].Highlight();
            yield return new WaitForSeconds(sequenceSpeed);
        }
    }


    public void CheckSequence(CubeButton cube)
    {
        int cubeIndex = System.Array.IndexOf(cubes, cube);  // Trouve l'index du cube cliqué
        userSequence.Add(cubeIndex);

        if (cubeIndex == randomSequence[currentStep])
        {
            currentStep++;
            if (currentStep >= randomSequence.Length)
            {
                SequenceSuccess();
            }
        }
        else
        {
            SequenceFailure();
        }
    }

    private void SequenceFailure()
    {
        Debug.Log("Failed!");
    }

    private void SequenceSuccess()
    {
        Debug.Log("Success!");
    }
}

