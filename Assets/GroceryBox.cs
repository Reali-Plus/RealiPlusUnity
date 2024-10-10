using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroceryBox : MonoBehaviour
{
    public List<GameObject> groceryList; // La liste des objets que le joueur doit collecter
    private List<GameObject> collectedItems = new List<GameObject>(); // Les objets correctement placés dans la boîte

    void OnTriggerEnter(Collider other)
    {
        GameObject enteringObject = other.gameObject;
        if (groceryList.Contains(enteringObject) && !collectedItems.Contains(enteringObject))
        {
            collectedItems.Add(enteringObject);
            Debug.Log($"{enteringObject.name} ajouté à la boîte.");
            CheckCompletion();
        }
        else
        {
            Debug.Log($"Objet incorrect ou déjà ajouté : {enteringObject.name}");
        }
    }

    void CheckCompletion()
    {
        if (collectedItems.Count == groceryList.Count)
        {
            Debug.Log("Tous les objets ont été collectés, niveau réussi !");
        }
    }
}
