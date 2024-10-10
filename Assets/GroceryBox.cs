using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Gestionnaire de validation des objets
public class GroceryBox : MonoBehaviour
{
    private List<GameObject> collectedItems = new List<GameObject>(); // Liste des objets correctement placés dans la boîte
    [SerializeField] private ShopManager shopManager; // Référence au ShopManager pour signaler les objets collectés
    [SerializeField] private GroceryListHandler groceryListHandler; // Référence au ShopManager pour signaler les objets collectés

    // Cette méthode est appelée quand un objet entre dans la boîte
    void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet qui entre dans la boîte fait partie de la liste d'épicerie
        if (groceryListHandler.GetGroceryList().Contains(other.gameObject))
        {
            if (!collectedItems.Contains(other.gameObject))
            {
                collectedItems.Add(other.gameObject);
                Debug.Log(other.gameObject.name + " ajouté à la boîte.");
                shopManager.CheckIfAllItemsCollected(collectedItems); // Appelle la vérification ici
            }
        }
        else
        {
            Debug.Log("Objet incorrect : " + other.gameObject.name);
        }
    }
}
