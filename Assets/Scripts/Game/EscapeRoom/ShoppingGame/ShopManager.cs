using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MiniGameManager
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
        Initialized();
    }

    private void Initialized()
    {
        groceryListHandler = GameObject.FindGameObjectWithTag("ShopManager").GetComponent<GroceryListHandler>();
        groceryBox = GameObject.FindGameObjectWithTag("GroceryBox").GetComponent<GroceryBox>();
        miniGamesController = GameObject.FindGameObjectWithTag("GamesController");
        itemsInStore = GameObject.FindGameObjectWithTag("ItemsInStore");
        wonObject = GameObject.FindGameObjectWithTag("Key");

        wonObject.SetActive(false);
        alreadyWon = false;

        allItemsAvailable.Clear();
        groceryBox.CollectedItems.Clear();
        PopulateItemList();
    }

    protected override void StartGame()
    {
        groceryListHandler.GenerateGroceryList(nbrItemsInGroceryList, allItemsAvailable);
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
        ResetGame();
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

    public void ResetItemPosition(GameObject item)
    {
        Quaternion currentRotation = miniGamesController.transform.rotation;

        if (originalPositions.ContainsKey(item))
        {
            Vector3 originalPosition = originalPositions[item];
            Vector3 adjustedPosition = currentRotation * (originalPosition - miniGamesController.transform.position);
            item.transform.position = miniGamesController.transform.position + adjustedPosition;
        }
    }

    protected override void ResetGame()
    {
        ResetItemsToOriginalPositions();

        wonObject.SetActive(false);
        alreadyWon = false;

        allItemsAvailable.Clear();
        groceryBox.CollectedItems.Clear();

        PopulateItemList();
        groceryListHandler.GenerateGroceryList(nbrItemsInGroceryList, allItemsAvailable);
        groceryListHandler.ResetDisplay();
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject item = collision.collider.gameObject;
        if (allItemsAvailable.Contains(item))
        {
            ResetItemPosition(item);
        }
    }
}
