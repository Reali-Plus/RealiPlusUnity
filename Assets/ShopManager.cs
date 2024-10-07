using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] GameObject listOfObjects;
    [SerializeField] Text groceryListText;
    [SerializeField] int nbrItemsInGroceryList = 3;//nombre d'objets dans la liste
    private List<GameObject> allItemsAvailable = new List<GameObject>(); //tout les objets disponibles à l'achat
    private List<GameObject> groceryList = new List<GameObject>();

    void Start()
    {
        foreach (Transform child in listOfObjects.transform)
        {
            allItemsAvailable.Add(child.gameObject);
        }

        GenerateGroceryList();
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
        string listText = "Items:\n";
        foreach (GameObject item in groceryList)
        {
            listText += "- " + item.name + "\n";
        }

        groceryListText.text = listText;
        Debug.Log(listText);
    }

}
