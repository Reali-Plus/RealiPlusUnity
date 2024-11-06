using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Gestionnaire de validation des objets
public class GroceryBox : MonoBehaviour
{
    private List<GameObject> collectedItems = new List<GameObject>(); // Liste des objets correctement plac�s dans la bo�te
    [SerializeField] private ShopManager shopManager; // R�f�rence au ShopManager pour signaler les objets collect�s
    [SerializeField] private GroceryListHandler groceryListHandler; // R�f�rence au ShopManager pour signaler les objets collect�s

    // Cette m�thode est appel�e quand un objet entre dans la bo�te
    void OnTriggerEnter(Collider other)
    {
        // V�rifie si l'objet qui entre dans la bo�te fait partie de la liste d'�picerie
        if (groceryListHandler.GetGroceryList().Contains(other.gameObject))
        {
            if (!collectedItems.Contains(other.gameObject))
            {
                collectedItems.Add(other.gameObject);
                Debug.Log(other.gameObject.name + " ajout� � la bo�te.");
                shopManager.CheckIfAllItemsCollected(collectedItems); // Appelle la v�rification ici
            }
        }
        else
        {
            Debug.Log("Objet incorrect : " + other.gameObject.name);
        }
    }
}
