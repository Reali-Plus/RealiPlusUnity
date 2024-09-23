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
    //private GameObject[] puzzleButton;
    //List<GameObject> sequenceUser; //sequence user 


    private void Start()
    {
        randomSequence = new int[sequenceDifficulty];
        generateRandomSequence();
    }

    //private void OnMouseDown()
    //{
    //    Debug.Log("Activate");
    //}

    public void generateRandomSequence()
    {
        int tempReference;
        for (int i = 0; i < sequenceDifficulty; i++)
        {
            tempReference = Random.Range(0, cubes.Length);
            randomSequence[i] = tempReference;
        }
        playSequence();

    }

    void playSequence()
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
        Debug.Log("CubeIndex:" + cubeIndex);
        userSequence.Add(cubeIndex);

        if (cubeIndex == randomSequence[currentStep])
        {
            Debug.Log("Sequence:" + randomSequence[currentStep]+ "\n------");
            currentStep++;
            Debug.Log("CurrentStep: " + currentStep);
            Debug.Log("randomSequence.Length: " + randomSequence.Length);
            if (currentStep == randomSequence.Length)
            {
                sequenceSuccess();
            }
        }
        else
        {
            sequenceFailure();
        }
    }

    void sequenceFailure()
    {
        Debug.Log("Failed!");
    }

    void sequenceSuccess()
    {
        Debug.Log("Success!");
    }


    //public void HighlightCube(int index)
    //{
    //    if (index >= 0 && index < cubes.Length)
    //    {
    //        cubes[index].Highlight(); // Appelle la méthode Highlight() du cube sélectionné
    //    }
    //}

    //// Exemple d'une fonction pour simuler une séquence de couleurs
    //public void PlaySequence(int[] sequence)
    //{
    //    foreach (int index in sequence)
    //    {
    //        HighlightCube(index); // Surbrillance des cubes en fonction de la séquence
    //    }
    //}
}

