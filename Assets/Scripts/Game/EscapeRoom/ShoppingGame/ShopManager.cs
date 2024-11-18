using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] int nbrItemsInGroceryList = 3;

    private GameObject itemsInStore;
    private GameObject wonObject;
    private GameObject miniGamesController;
    private GroceryBox groceryBox;
    private GroceryListHandler groceryListHandler;
    private List<GameObject> allItemsAvailable = new List<GameObject>();
    private Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>();

    public bool alreadyWon = false;

    private void Start()
    {
        groceryListHandler = GameObject.FindGameObjectWithTag("ShopManager").GetComponent<GroceryListHandler>();
        groceryBox = GameObject.FindGameObjectWithTag("GroceryBox").GetComponent<GroceryBox>();
        miniGamesController = GameObject.FindGameObjectWithTag("GamesController");
        itemsInStore = GameObject.FindGameObjectWithTag("ItemsInStore");
        wonObject = GameObject.FindGameObjectWithTag("Key");

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
        yield return new WaitForSecondsRealtime(1);
        StartingState();
    }

    private void ResetItemsToOriginalPositions()
    {
       if (miniGamesController != null)
        {
            Quaternion currentRotation = miniGamesController.transform.rotation;

            foreach (var item in allItemsAvailable)
            {
                Vector3 originalPosition = originalPositions[item];
                Vector3 adjustedPosition = currentRotation * (originalPosition - miniGamesController.transform.position);
                item.transform.position = miniGamesController.transform.position + adjustedPosition;
            }
        }
    }
}
