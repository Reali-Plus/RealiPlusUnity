using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroceryBox : MonoBehaviour
{
    public List<GameObject> groceryList; // La liste des objets que le joueur doit collecter
    private List<GameObject> collectedItems = new List<GameObject>(); // Les objets correctement plac�s dans la bo�te

    void OnTriggerEnter(Collider other)
    {
        GameObject enteringObject = other.gameObject;
        if (groceryList.Contains(enteringObject) && !collectedItems.Contains(enteringObject))
        {
            collectedItems.Add(enteringObject);
            Debug.Log($"{enteringObject.name} ajout� � la bo�te.");
            CheckCompletion();
        }
        else
        {
            Debug.Log($"Objet incorrect ou d�j� ajout� : {enteringObject.name}");
        }
    }

    void CheckCompletion()
    {
        if (collectedItems.Count == groceryList.Count)
        {
            Debug.Log("Tous les objets ont �t� collect�s, niveau r�ussi !");
        }
    }
}
