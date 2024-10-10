using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroceryListHandler : MonoBehaviour
{
    //generer la liste d<epicerie et le display dans la scene
    //pourrait ajouter un check lorsque l objet est dans la boite...
    [SerializeField] Text groceryListText;
    private List<GameObject> groceryList = new List<GameObject>();

    // Genere la liste d'epicerie
    public void GenerateGroceryList(int nbrItemsInGroceryList, List<GameObject> allItems)
    {
        if (allItems.Count < nbrItemsInGroceryList)
        {
            Debug.LogWarning("Not enough items to select from!");
            return;
        }

        List<GameObject> itemsToSelectFrom = new List<GameObject>(allItems);
        groceryList = GetRandomItems(itemsToSelectFrom, nbrItemsInGroceryList);
    }

    // Sélectionne des objets aléatoires sans doublons
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

    public void DisplayGroceryList()
    {
        string listText = "Items:\n";
        foreach (GameObject item in groceryList)
        {
            listText += "- " + item.name + "\n";
        }

        groceryListText.text = listText;
        Debug.Log(listText);
    }

    //a verifier l'utilite
    public List<GameObject> GetGroceryList()
    {
        return groceryList;
    }
}
