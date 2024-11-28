using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MiniGameManager
{
    [SerializeField] int nbrItemsInGroceryList = 3;
    [SerializeField] private TextMeshProUGUI successMessage;

    [SerializeField]
    private GameObject itemsInStore;
    [SerializeField]
    private GameObject miniGamesController;
    [SerializeField]
    private GroceryBox groceryBox;
    [SerializeField]
    private GroceryListHandler groceryListHandler;
    private List<GameObject> allItemsAvailable = new List<GameObject>();
    private Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>();

    public bool alreadyWon = false;

    public override void Initialize()
    {
        successMessage.gameObject.SetActive(false);
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
        successMessage.gameObject.SetActive(true);
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

        successMessage.gameObject.SetActive(false);
        alreadyWon = false;

        allItemsAvailable.Clear();
        groceryBox.CollectedItems.Clear();

        PopulateItemList();
        groceryListHandler.GenerateGroceryList(nbrItemsInGroceryList, allItemsAvailable);
        groceryListHandler.ResetDisplay();
    }

    public void ReturnGroceryItem(GameObject item)
    {
        if (allItemsAvailable.Contains(item))
        {
            ResetItemPosition(item);
        }
    }
}
