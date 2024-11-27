using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private bool isStartCube;
    [SerializeField] private bool isQuitCube;

    void OnCollisionEnter()
    {
        HandleInteraction();
    }

    private void OnMouseDown()
    {
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        if (isStartCube)
        {
            StartGame();
        }
        else if (isQuitCube)
        {
            QuitGame();
        }
    }

    private void StartGame()
    {
        GameManager.Instance.StartFirstGame();
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
