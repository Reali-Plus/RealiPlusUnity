using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] int nbrItemsInGroceryList = 3;

    private GameObject itemsInStore;
    private GameObject wonObject;
    private GroceryListHandler groceryListHandler;
    private List<GameObject> allItemsAvailable = new List<GameObject>();
    private Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>();

    public bool alreadyWon = false;

    private void Start()
    {
        groceryListHandler = GameObject.FindGameObjectWithTag("ShopManager").GetComponent<GroceryListHandler>();
        wonObject = GameObject.FindGameObjectWithTag("Key");
        itemsInStore = GameObject.FindGameObjectWithTag("ItemsInStore");

        StartingState();
    }

    private void PopulateItemList()
    {
        foreach (Transform child in itemsInStore.transform)
        {
            GameObject item = child.gameObject;
            allItemsAvailable.Add(child.gameObject);
            if (!originalPositions.ContainsKey(item))
            {
                originalPositions[item] = item.transform.position;
            }
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
            }
        }
    }

    private void OnAllItemsCollected()
    {
        alreadyWon = true;
        wonObject.SetActive(true);
        StartCoroutine(WaitAndResetGame());
    }

    private IEnumerator WaitAndResetGame()
    {
        yield return new WaitForSecondsRealtime(2);
        Debug.Log("RESET");
        StartingState();
        ResetItemsToOriginalPositions();
    }

    private void ResetItemsToOriginalPositions()
    {
        foreach (GameObject item in allItemsAvailable)
        {
            if (originalPositions.TryGetValue(item, out Vector3 originalPosition))
            {
                item.transform.position = originalPosition;
            }
        }
    }

    private void StartingState()
    {
        wonObject.SetActive(false);
        alreadyWon = false;
        allItemsAvailable.Clear();

        PopulateItemList();
        groceryListHandler.GenerateGroceryList(nbrItemsInGroceryList, allItemsAvailable);
    }
}
