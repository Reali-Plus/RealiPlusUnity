using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] GameObject itemsInStore; //Parent de tous les objets disponible
    [SerializeField] Text groceryListText;
    [SerializeField] int nbrItemsInGroceryList = 3;//nombre d'objets dans la liste
    public List<GameObject> allItemsAvailable = new List<GameObject>(); //tout les objets disponibles à l'achat
    private List<GameObject> groceryList = new List<GameObject>();

    void Start()
    {
        PopulateItemList();
        GenerateGroceryList();
        DisplayGroceryList();
    }

    // liste de tout les objets disponible à partir des enfants de ItemsInStore
    private void PopulateItemList()
    {
        foreach (Transform child in itemsInStore.transform)
        {
            allItemsAvailable.Add(child.gameObject);
        }
    }

    // Genere la liste d'epicerie
    private void GenerateGroceryList()
    {
        if (allItemsAvailable.Count < nbrItemsInGroceryList)
        {
            Debug.LogWarning("Not enough items to select from!");
            return;
        }

        List<GameObject> itemsToSelectFrom = new List<GameObject>(allItemsAvailable);
        groceryList = GetRandomItems(itemsToSelectFrom, nbrItemsInGroceryList);
    }

    // Sélectionne des objets aléatoires sans répétition
    private List<GameObject> GetRandomItems(List<GameObject> sourceList, int itemCount)
    {
        List<GameObject> selectedItems = new List<GameObject>();
        for (int i = 0; i < itemCount; i++)
        {
            int randomIndex = Random.Range(0, sourceList.Count);
            selectedItems.Add(sourceList[randomIndex]);
            sourceList.RemoveAt(randomIndex);
        }
        return selectedItems;
    }

    private void DisplayGroceryList()
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
