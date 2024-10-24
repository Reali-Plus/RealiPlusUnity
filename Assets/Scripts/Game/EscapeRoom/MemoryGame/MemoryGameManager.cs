using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGameManager : MonoBehaviour
{
    [SerializeField] private CubeButton[] cubes;
    [SerializeField] private AudioSource successSound;
    [SerializeField] private AudioSource failureSound;

    //private GameManager gameManager;
    private SequenceManager sequenceManager;
    private GameObject successObject;        
    private List<int> userSequence = new List<int>();
    private int currentStep = 0;
    private bool playerWon = false;
    private bool isAlreadyPlayed = false;

    private void Start()
    {
        sequenceManager = GameObject.FindGameObjectWithTag("MemoryManager").GetComponent<SequenceManager>();
        //gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        successObject = GameObject.FindGameObjectWithTag("Key");

        isAlreadyPlayed = false;
        sequenceManager.SetupSequence(cubes);
        successObject.SetActive(false);
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

        int cubeIndex = System.Array.IndexOf(cubes, cube);
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
        successObject.SetActive(true);
        successSound.Play();
        GameSceneManager.LoadScene("ShoppingScene");
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

