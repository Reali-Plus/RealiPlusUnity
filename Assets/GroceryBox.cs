using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroceryBox : MonoBehaviour
{
    private ShopManager shopManager;
    private GroceryListHandler groceryListHandler;
    private List<GameObject> collectedItems = new List<GameObject>();

    private void Start()
    {
        shopManager = GameObject.FindGameObjectWithTag("ShopManager").GetComponent<ShopManager>();
        groceryListHandler = GameObject.FindGameObjectWithTag("ShopManager").GetComponent<GroceryListHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!shopManager.alreadyWon)
        {
            if (groceryListHandler.GetGroceryList().Contains(other.gameObject))
            {
                if (!collectedItems.Contains(other.gameObject))
                {
                    collectedItems.Add(other.gameObject);
                    groceryListHandler.MarkItemAsCorrect(other.gameObject);
                    Debug.Log(other.gameObject.name + " ajouté à la boîte et est dans la liste.");
                }
            }
            else
            {
                collectedItems.Add(other.gameObject);
                groceryListHandler.MarkItemAsIncorrect(other.gameObject);
                Debug.Log("Objet incorrect : " + other.gameObject.name);
            }
            shopManager.CheckIfAllItemsCollected(collectedItems);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!shopManager.alreadyWon)
        {
            collectedItems.Remove(other.gameObject);
            Debug.Log(other.gameObject.name + " retiré de la boîte.");
            if (groceryListHandler.GetGroceryList().Contains(other.gameObject))
            {
                if (!collectedItems.Contains(other.gameObject))
                {
                    groceryListHandler.MarkItemAsCorrect(other.gameObject);
                }
            }
            else
            {
                groceryListHandler.MarkItemAsIncorrect(other.gameObject);
            }
            shopManager.CheckIfAllItemsCollected(collectedItems);
        }
    }
}
