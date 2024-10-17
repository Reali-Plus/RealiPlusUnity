using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public GameObject itemsInStore;
    private Camera mainCamera;
    private bool isDragging = false;
    private GameObject selectedObject;
    private Vector3 offset;
    private List<GameObject> moveableItems = new List<GameObject>();

    void Start()
    {
        mainCamera = Camera.main;

        PopulateItemList(itemsInStore.transform, moveableItems);
    }

    void Update()
    {
        HandleObjectDragging();
    }

    //TODO: change with the hand model
    private void HandleObjectDragging()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (TryGetHitObject(out hit))
            {
                GameObject hitObject = hit.collider.gameObject;

                if (moveableItems.Contains(hitObject))
                {
                    StartDragging(hitObject);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging && selectedObject != null)
        {
            selectedObject.transform.position = GetMouseWorldPosition() + offset;
        }
    }
    private bool TryGetHitObject(out RaycastHit hit)
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit);
    }

    private void StartDragging(GameObject hitObject)
    {
        selectedObject = hitObject;
        isDragging = true;
        offset = selectedObject.transform.position - GetMouseWorldPosition();
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mainCamera.WorldToScreenPoint(selectedObject.transform.position).z;
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }
    private void PopulateItemList(Transform parent, List<GameObject> itemList)
    {
        foreach (Transform child in parent)
        {
            itemList.Add(child.gameObject);
        }
    }
}

