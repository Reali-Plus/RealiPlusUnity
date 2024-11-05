using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int nbrLevel = 2;
    [SerializeField] private GameObject firstGame;
    [SerializeField] private GameObject secondGame;
    public int KeysPossessed { get; set; } = 0;
    public GameObject mainMenu;
    public GameObject rotatingObject;

    private enum GameState { Menu, FirstGame, SecondGame }
    private GameState currentState;


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
        currentState = GameState.Menu;
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

    public void AddKey()
    {
        KeysPossessed++;
        Debug.Log("Keys Possessed: " + KeysPossessed);
        //TODO: Added visual content like UI for nbr of key possessed

        if (KeysPossessed >= nbrLevel)
        {
            LoadDoorScene();
        }
    }

    private void RotateObject()
    {
        if (rotatingObject != null)
        {
            rotatingObject.transform.Rotate(0, 90, 0);
        }
    }

    private void LoadDoorScene()
    {
        GameSceneManager.LoadScene("DoorScene");
    }
}
