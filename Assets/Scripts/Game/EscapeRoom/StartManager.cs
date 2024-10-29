using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    public Button startMemoryGameButton;

    void Start()
    {
        startMemoryGameButton.onClick.AddListener(LoadMemoryGame);
    }

    void LoadMemoryGame()
    {
        GameManager.Instance.LoadMemoryScene();
    }
}
