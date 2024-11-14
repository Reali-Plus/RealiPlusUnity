using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GroceryListHandler : MonoBehaviour
{
    [SerializeField] private TextMeshPro groceryListText;
    [SerializeField] private TextMeshPro incorrectItemsText;
    private List<GameObject> groceryList = new List<GameObject>();
    private List<GameObject> correctItems = new List<GameObject>();
    private List<GameObject> incorrectItems = new List<GameObject>();

    public void GenerateGroceryList(int nbrItemsInGroceryList, List<GameObject> allItems)
    {
        if (allItems.Count < nbrItemsInGroceryList)
        {
            Debug.LogWarning("Not enough items");
            return;
        }

        List<GameObject> itemsToSelectFrom = new List<GameObject>(allItems);
        groceryList = GetRandomItems(itemsToSelectFrom, nbrItemsInGroceryList);
        UpdateGroceryListUI();
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

    public void MarkItemAsCorrect(GameObject item)
    {
        if (!correctItems.Contains(item))
        {
            correctItems.Add(item);
            UpdateGroceryListUI();
        }
        else
        {
            correctItems.Remove(item);
            UpdateGroceryListUI();
        }
    }

    public void MarkItemAsIncorrect(GameObject incorrectItem)
    {
        if (!incorrectItems.Contains(incorrectItem))
        {
            incorrectItems.Add(incorrectItem);
            UpdateIncorrectItemsUI();
        }
        else
        {
            incorrectItems.Remove(incorrectItem);
            UpdateIncorrectItemsUI();
        }
    }

    public List<GameObject> GetGroceryList()
    {
        return groceryList;
    }

    private void UpdateGroceryListUI()
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

    private void UpdateIncorrectItemsUI()
    {
        string incorrectListText = "Items incorrect:\n";
        foreach (GameObject incorrectItem in incorrectItems)
        {
            incorrectListText += "<color=red>" + incorrectItem.name + "</color>\n";
        }

        incorrectItemsText.text = incorrectListText;
    }

    public void ResetDisplay()
    {
        correctItems.Clear();
        incorrectItems.Clear();
        UpdateGroceryListUI(); 
        UpdateIncorrectItemsUI();
    }
}
