using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] int nbrItemsInGroceryList = 3;
    [SerializeField] private GroceryBox groceryBox;

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

    private void StartingState()
    {
        wonObject.SetActive(false);
        alreadyWon = false;
        allItemsAvailable.Clear();
        groceryBox.collectedItems.Clear();

        PopulateItemList();
        groceryListHandler.GenerateGroceryList(nbrItemsInGroceryList, allItemsAvailable);
        groceryListHandler.ResetDisplay();
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
                Debug.Log("Count:" + groceryListHandler.GetGroceryList().Count);
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
        ResetItemsToOriginalPositions();
        StartCoroutine(WaitAndResetGame());
    }

    private IEnumerator WaitAndResetGame()
    {
        yield return new WaitForSecondsRealtime(2);
        StartingState();

        //StartCoroutine(Wai()); //solution pas ideal...
    }

    //private IEnumerator Wai()
    //{
    //    yield return new WaitForSecondsRealtime(0.5f);
    //    groceryListHandler.ResetDisplay();
    //}

    private void ResetItemsToOriginalPositions()
    {
        for (int i = 0; i < allItemsAvailable.Count; i++)
        {
            allItemsAvailable[i].transform.position = originalPositions[allItemsAvailable[i]];
        }
    }
}
