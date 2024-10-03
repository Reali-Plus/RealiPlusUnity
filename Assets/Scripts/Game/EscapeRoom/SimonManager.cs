using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonManager : MonoBehaviour
{
    [SerializeField] CubeButton[] cubes; // Tableau pour les cubes
    [SerializeField] SequenceManager sequenceManager;
    [SerializeField] GameObject successObject;

    private List<int> userSequence = new List<int>();  // Sequence entrée par l'utilisateur
    private int currentStep = 0;
    private bool playerWon = false;
    private bool isAlreadyPlayed = false;

    private void Start()
    {
        isAlreadyPlayed = false;
        sequenceManager.SetupSequence(cubes);
        successObject.SetActive(false); //regarder pour l'ajouter dynamiquement + asset
    }

    public void PlaySequence()
    {
        if (!sequenceManager.isSequencePlaying) 
        {
            sequenceManager.PlaySequence();  // Délègue à SequenceManager la gestion de la séquence
            isAlreadyPlayed = true;
        }
    }

    public void CheckSequence(CubeButton cube)
    {
        if (sequenceManager.isSequencePlaying) return;
        if (!isAlreadyPlayed) return;
        int cubeIndex = System.Array.IndexOf(cubes, cube);  // Trouve l'index du cube cliqué
        userSequence.Add(cubeIndex);
        if (!playerWon)
        {
            if (cubeIndex == sequenceManager.GetCurrentSequenceStep(currentStep))
            {
                currentStep++;
                if (currentStep >= sequenceManager.GetSequenceLength())
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
        sequenceManager.isSequencePlaying = true; //bloque les boutons quand on win
        successObject.SetActive(true);
        Debug.Log("Success!");
    }

    private void SequenceFailure()
    {
        playerWon = false;
        Debug.Log("Failed!");
        ResetSequence();
    }
    private void ResetSequence()
    {
        currentStep = 0;
        userSequence.Clear();
        isAlreadyPlayed = false;
        sequenceManager.isSequencePlaying = false;
        sequenceManager.GenerateRandomSequence(); // Génère une nouvelle séquence aléatoire 
        //Debug.Log("Restart sequence, play another:");
        //new WaitForSeconds(30);
        //PlaySequence(); // Rejoue la nouvelle séquence
    }

}

