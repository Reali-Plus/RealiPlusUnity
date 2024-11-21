using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGameManager : MiniGameManager
{
    [SerializeField] private CubeButton[] cubes;
    [SerializeField] private AudioSource successSound;
    [SerializeField] private AudioSource failureSound;

    private SequenceManager sequenceManager;
    //private GameObject successObject;        
    private List<int> userSequence = new List<int>();
    private int currentStep = 0;
    private bool playerWon = false;
    private bool isAlreadyPlayed = false;

    private void Start()
    {
        Initialized();
    }

    private void Initialized()
    {
        sequenceManager = GameObject.FindGameObjectWithTag("MemoryManager").GetComponent<SequenceManager>();
        //successObject = GameObject.FindGameObjectWithTag("Key");

        isAlreadyPlayed = false;
        //successObject.SetActive(false);
    }

    protected override void StartGame()
    {
        sequenceManager.SetupSequence(cubes);
    }

    public bool IsStartButton(CubeButton cube)
    {
        return cube.name == "StartButton";
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
        if (sequenceManager.isSequencePlaying || playerWon || !isAlreadyPlayed) return;

        int cubeIndex = Array.IndexOf(cubes, cube);
        userSequence.Add(cubeIndex);

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

    private void SequenceSuccess()
    {
        playerWon = true;
        sequenceManager.isSequencePlaying = true;
       
        //successObject.SetActive(true);
        successSound.Play();

        StartCoroutine(WaitAndResetGame());
    }

    private IEnumerator WaitAndResetGame()
    {
        yield return new WaitForSecondsRealtime(1);
        ResetGame();
    }

    private void SequenceFailure()
    {
        playerWon = false;
        failureSound.Play();
        ShowFailureVisuals();
        ResetGame();
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
        isAlreadyPlayed = false;
        playerWon = false;
        sequenceManager.isSequencePlaying = false;
        //successObject.SetActive(false);
        userSequence.Clear();
        sequenceManager.GenerateRandomSequence();
    }

    protected override void ResetGame()
    {
        ResetSequence();
    }
}

