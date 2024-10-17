using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroceryBox : MonoBehaviour
{
    private List<GameObject> collectedItems = new List<GameObject>();
    [SerializeField] private ShopManager shopManager;
    [SerializeField] private GroceryListHandler groceryListHandler;

    void OnTriggerEnter(Collider other)
    {
        if (!shopManager.alreadyWon)
        {
            if (groceryListHandler.GetGroceryList().Contains(other.gameObject))
            {
                if (!collectedItems.Contains(other.gameObject))
                {
                    collectedItems.Add(other.gameObject);
                    Debug.Log(other.gameObject.name + " ajouté à la boîte et est dans la liste.");
                }
            }
            else
            {
                collectedItems.Add(other.gameObject);
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
        }
        shopManager.CheckIfAllItemsCollected(collectedItems);
    }
}
