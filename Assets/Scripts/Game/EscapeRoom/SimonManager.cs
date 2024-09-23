using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonManager : MonoBehaviour
{
    [SerializeField] CubeButton[] cubes; // Tableau pour les cubes
    [SerializeField] int sequenceDifficulty = 5; //difficulte
    [SerializeField] float sequenceSpeed = 0.25f; //vitesse
    private int[] randomSequence;  //sequence aleatoire

    //private GameObject[] puzzleButton;
    //List<GameObject> sequenceUser; //sequence user 


    private void Start()
    {
        randomSequence = new int[sequenceDifficulty];
        generateSimonSequence();
    }

    //private void OnMouseDown()
    //{
    //    Debug.Log("Activate");
    //}

    public void generateSimonSequence()
    {
        int tempReference;
        for (int i = 0; i < sequenceDifficulty; i++)
        {
            tempReference = Random.Range(0, cubes.Length);
            randomSequence[i] = tempReference;
        }
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

