using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject firstGame;
    [SerializeField] private GameObject secondGame;

    private enum GameState { Menu, FirstGame, SecondGame }
    private GameState currentState;
    private GameObject mainMenu;
    private GameObject player;


    #region Singleton
    private static GameManager _instance;
    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(this);
    } 

    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("GameManager is Null");
            }
            return _instance;
        }
    }
    #endregion

    void Start()
    {
        mainMenu = GameObject.FindGameObjectWithTag("MainMenu");
        player = GameObject.FindGameObjectWithTag("Player");
        currentState = GameState.SecondGame;
        UpdateGameState();
    }

    public void StartFirstGame()
    {
        if (currentState == GameState.Menu)
        {
            currentState = GameState.FirstGame;
            RotateObject();
            UpdateGameState();
        }
    }

    public void CompleteFirstGame()
    {
        if (currentState == GameState.FirstGame)
        {
            currentState = GameState.SecondGame;
            RotateObject();
            UpdateGameState();
        }
    }

    private void UpdateGameState()
    {
        mainMenu.SetActive(currentState == GameState.Menu);
        firstGame.SetActive(currentState == GameState.FirstGame);
        secondGame.SetActive(currentState == GameState.SecondGame);
    }

    private void RotateObject()
    {
        if (player != null)
        {
            player.transform.Rotate(0, 90, 0);
        }
    }

}
