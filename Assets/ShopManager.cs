using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] GameObject itemsInStore;
    [SerializeField] int nbrItemsInGroceryList = 3;

    private GameObject wonObject;
    private GroceryListHandler groceryListHandler;
    private List<GameObject> allItemsAvailable = new List<GameObject>();
    
    public bool alreadyWon = false;

    private void Start()
    {
        groceryListHandler = GameObject.FindGameObjectWithTag("ShopManager").GetComponent<GroceryListHandler>();
        wonObject = GameObject.FindGameObjectWithTag("Key");

        wonObject.SetActive(false);
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
        HashSet<GameObject> collectedSet = new HashSet<GameObject>(collectedItems);
        HashSet<GameObject> groceryListSet = new HashSet<GameObject>(groceryListHandler.GetGroceryList());
        if (!alreadyWon)
        {
            if (collectedItems.Count == groceryListHandler.GetGroceryList().Count) 
            { 
                if (collectedSet.SetEquals(groceryListSet))
                {
                     OnAllItemsCollected();
                }
                else
                {
                    Debug.Log("Il y a des objets incorrects ou manquants dans la boîte !");
                }
            }
        }
    }

    private void OnAllItemsCollected()
    {
        alreadyWon = true;
        wonObject.SetActive(true);
        foreach (Transform child in itemsInStore.transform)
        {
            //TODO: block interaction
        }
    }
}
