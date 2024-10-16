using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Gestionnaire de validation des objets
public class GroceryBox : MonoBehaviour
{
    private List<GameObject> collectedItems = new List<GameObject>();
    [SerializeField] private ShopManager shopManager;
    [SerializeField] private GroceryListHandler groceryListHandler;

    void OnTriggerEnter(Collider other)
    {
        if (groceryListHandler.GetGroceryList().Contains(other.gameObject))
        {
            if (!collectedItems.Contains(other.gameObject))
            {
                collectedItems.Add(other.gameObject);
                Debug.Log(other.gameObject.name + " ajouté à la boîte.");
                shopManager.CheckIfAllItemsCollected(collectedItems);
            }
        }
        else
        {
            Debug.Log("Objet incorrect : " + other.gameObject.name);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (collectedItems.Contains(other.gameObject))
        {
            collectedItems.Remove(other.gameObject);
            Debug.Log(other.gameObject.name + " retiré de la boîte.");
        }
    }
}
