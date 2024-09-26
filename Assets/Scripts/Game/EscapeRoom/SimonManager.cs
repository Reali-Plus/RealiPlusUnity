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

    bool playerWon = false;

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
        if (!playerWon)
        {
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
        else
            Debug.Log("You already won!!");
    }

    private void SequenceSuccess()
    {
        playerWon = true;
        Debug.Log("Success!");
    }

    private void SequenceFailure()
    {
        playerWon = false;
        Debug.Log("Failed!");
    }

}

