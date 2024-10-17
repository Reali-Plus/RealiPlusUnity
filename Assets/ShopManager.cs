using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] GameObject itemsInStore; //Parent de tous les objets disponible
    [SerializeField] GroceryListHandler groceryListHandler;
    [SerializeField] int nbrItemsInGroceryList = 3;
    private List<GameObject> allItemsAvailable = new List<GameObject>(); //tout les objets disponibles à l'achat
    public bool alreadyWon = false;

    void Start()
    {
        alreadyWon = false;
        allItemsAvailable.Clear();
        PopulateItemList();
        groceryListHandler.GenerateGroceryList(nbrItemsInGroceryList, allItemsAvailable);
    }

    private void PopulateItemList()
    {
        foreach (Transform child in itemsInStore.transform)
        {
            allItemsAvailable.Add(child.gameObject);
        }
    }

    public void CheckIfAllItemsCollected(List<GameObject> collectedItems)
    {
        if (!alreadyWon)
        {
            if (collectedItems.Count == groceryListHandler.GetGroceryList().Count)
            {
                OnAllItemsCollected(); 
            }
        }
    }

    private void OnAllItemsCollected()
    {
        Debug.Log("Niveau réussi !");
        alreadyWon = true;
        foreach (Transform child in itemsInStore.transform)
        {
            //block interaction
        }
    }
}
