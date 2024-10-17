using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGameManager : MonoBehaviour
{
    [SerializeField] private CubeButton[] cubes;
    [SerializeField] private GameObject successObject;
    [SerializeField] private AudioSource successSound;
    [SerializeField] private AudioSource failureSound;

    private SequenceManager sequenceManager;
    private List<int> userSequence = new List<int>();
    private int currentStep = 0;
    private bool playerWon = false;
    private bool isAlreadyPlayed = false;

    private void Start()
    {
        sequenceManager = GameObject.FindGameObjectWithTag("MemoryManager").GetComponent<SequenceManager>();
        isAlreadyPlayed = false;
        sequenceManager.SetupSequence(cubes);
        successObject.SetActive(false); //TODO: regarder pour l'ajouter dynamiquement + asset
    }

    public void PlaySequence()
    {
        if (!sequenceManager.isSequencePlaying) 
        {
            sequenceManager.PlaySequence();
            isAlreadyPlayed = true;
        }
    }

    public void CheckSequence(CubeButton cube)
    {
        if (sequenceManager.isSequencePlaying) return;
        if (!isAlreadyPlayed) return;
        int cubeIndex = System.Array.IndexOf(cubes, cube);
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
        sequenceManager.isSequencePlaying = true;
        successObject.SetActive(true);
        successSound.Play();
    }

    private void SequenceFailure()
    {
        playerWon = false;
        failureSound.Play();
        ShowFailureVisuals();
        ResetSequence();
    }

    private void ShowFailureVisuals()
    {
        foreach (CubeButton cube in cubes)
        {
            cube.ShowFailureColor();
        }
    }

    private void ResetSequence()
    {
        currentStep = 0;
        userSequence.Clear();
        isAlreadyPlayed = false;
        sequenceManager.isSequencePlaying = false;
        sequenceManager.GenerateRandomSequence();
    }
}

