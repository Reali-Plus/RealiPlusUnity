using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] GameObject listOfObjects;
    [SerializeField] int nbrItemsInGroceryList = 3;//nombre d'objets dans la liste
    private List<GameObject> allItemsAvailable = new List<GameObject>(); //tout les objets disponibles à l'achat
    private List<GameObject> groceryList = new List<GameObject>(); 

    void Start()
    {
        // Récupérer tous les objets enfants
        foreach (Transform child in listOfObjects.transform)
        {
            allItemsAvailable.Add(child.gameObject);
        }
        // Générer la liste d'épicerie aléatoire
        GenerateGroceryList();

        // Afficher la liste d'épicerie
        DisplayGroceryList();
    }

    void GenerateGroceryList()
    {
        if (allItemsAvailable.Count < nbrItemsInGroceryList)
        {
            Debug.LogWarning("Not enough items to select from!");
            return;
        }

        List<GameObject> itemsToSelectFrom = new List<GameObject>(allItemsAvailable);

        for (int i = 0; i < nbrItemsInGroceryList; i++)
        {
            int randomIndex = Random.Range(0, itemsToSelectFrom.Count);
            groceryList.Add(itemsToSelectFrom[randomIndex]);
            itemsToSelectFrom.RemoveAt(randomIndex); // pour eviter les doublons
        }
    }

    void DisplayGroceryList()
    {
        Debug.Log("Grocery List:");
        foreach (GameObject item in groceryList)
        {
            Debug.Log("- " + item.name);
        }
    }
}
