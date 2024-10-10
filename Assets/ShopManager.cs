using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] GameObject itemsInStore; //Parent de tous les objets disponible
    [SerializeField] GroceryListHandler groceryListHandler;
    [SerializeField] GroceryBox groceryBox;
    [SerializeField] int nbrItemsInGroceryList = 3;
    private List<GameObject> allItemsAvailable = new List<GameObject>(); //tout les objets disponibles à l'achat

    void Start()
    {
        allItemsAvailable.Clear();
        PopulateItemList();
        groceryListHandler.GenerateGroceryList(nbrItemsInGroceryList, allItemsAvailable); // Génère la liste
        groceryListHandler.DisplayGroceryList();  // Affiche la liste
    }

    // liste de tous les objets disponible
    private void PopulateItemList()
    {
        foreach (Transform child in itemsInStore.transform)
        {
            allItemsAvailable.Add(child.gameObject);
        }
    }

    public void CheckIfAllItemsCollected(List<GameObject> collectedItems)
    {
        if (collectedItems.Count == groceryListHandler.GetGroceryList().Count)
        {
            OnAllItemsCollected(); // Signale que tous les objets ont été collectés
        }
    }

    private void OnAllItemsCollected()
    {
        Debug.Log("Niveau réussi !");
    }
}
