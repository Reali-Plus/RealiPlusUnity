using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject parentObject;
    public List<GameObject> allItems = new List<GameObject>();

    void Start()
    {
        foreach (Transform child in parentObject.transform)
        {
            allItems.Add(child.gameObject);
            Debug.Log("items found: " + child.gameObject);

        }

        // Maintenant tu peux utiliser allItems pour créer ta liste d'épicerie aléatoire.
        Debug.Log("Total items found: " + allItems.Count);
    }
}
