using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GroceryListHandler : MonoBehaviour
{
    [SerializeField] private TextMeshPro groceryListText;
    private List<GameObject> groceryList = new List<GameObject>();
    private List<GameObject> correctItems = new List<GameObject>();
    private List<GameObject> incorrectItems = new List<GameObject>();

    public void GenerateGroceryList(int nbrItemsInGroceryList, List<GameObject> allItems)
    {
        if (allItems.Count < nbrItemsInGroceryList)
        {
            Debug.LogWarning("Not enough items to select from!");
            return;
        }

        List<GameObject> itemsToSelectFrom = new List<GameObject>(allItems);
        groceryList = GetRandomItems(itemsToSelectFrom, nbrItemsInGroceryList);
        DisplayGroceryList();
    }

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
            if (correctItems.Contains(item))
            {
                listText += "- " + "<s>" + item.name + "</s>\n";
            }
            else
            {
                listText += "- " + item.name + "\n";
            }
        }

        groceryListText.text = listText;
    }

    public void MarkItemAsCorrect(GameObject item)
    {
        if (!correctItems.Contains(item))
        {
            correctItems.Add(item);
            DisplayGroceryList();
        }
        else
        {
            correctItems.Remove(item);
            DisplayGroceryList();
        }
    }


    public List<GameObject> GetGroceryList()
    {
        return groceryList;
    }
}
