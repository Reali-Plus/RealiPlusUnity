using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public void OnPlayButton()
    {
        LoadMemoryGame();
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    private void LoadMemoryGame()
    {
        GameManager.Instance.LoadMemoryScene();
    }
}
