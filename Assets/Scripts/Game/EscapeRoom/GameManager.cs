using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MiniGameManager firstGame;
    [SerializeField] private MiniGameManager secondGame;
    [SerializeField] private MiniGameManager thirdGame;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject miniGamesController;
    private bool interactionsEnabled = true;

    private enum GameState
    {
        Menu = 0,
        FirstGame = 1,
        SecondGame = 2,
        ThirdGame = 3
    }
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
            if (_instance == null)
            {
                Debug.LogError("GameManager is Null");
            }
            return _instance;
        }
    }

    public bool InteractionsEnabled { get => interactionsEnabled; set => interactionsEnabled = value; }
    #endregion

    void Start()
    {
        interactionsEnabled = false;
        currentState = GameState.Menu;
        UpdateGameState();
        mainMenu.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!interactionsEnabled) return;

        //NUM NAV
        if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
        {
            SwitchToState(GameState.Menu);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            SwitchToState(GameState.FirstGame);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            SwitchToState(GameState.SecondGame);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) ||  Input.GetKeyDown(KeyCode.Keypad3))
        {
            SwitchToState(GameState.ThirdGame);
        }

        //ARROW NAV
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Navigate(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Navigate(1);
        }
    }

    public void StartFirstGame()
    {
        if (currentState == GameState.Menu)
        {
            SwitchToState(GameState.FirstGame);
        }
    }

    private void UpdateGameState()
    {
        mainMenu.gameObject.SetActive(currentState == GameState.Menu);
        firstGame.gameObject.SetActive(currentState == GameState.FirstGame);
        secondGame.gameObject.SetActive(currentState == GameState.SecondGame);
        thirdGame.gameObject.SetActive(currentState == GameState.ThirdGame);
    }

    private void SwitchToState(GameState targetState)
    {
        if (currentState != targetState)
        {
            QuitCurrentGame(currentState);
            RotateObject(targetState);
            currentState = targetState;
            StartNewGame(targetState);
            UpdateGameState();
        }
    }

    private void StartNewGame(GameState state)
    {
        MiniGameManager game = GetGameByState(state);
        if (game != null)
        {
            game.Starting();
        }
    }

    private void QuitCurrentGame(GameState state)
    {
        MiniGameManager game = GetGameByState(state);
        if (game != null)
        {
            game.Restart();
        }
    }

    private MiniGameManager GetGameByState(GameState state)
    {
        return state switch
        {
            GameState.FirstGame => firstGame,
            GameState.SecondGame => secondGame,
            GameState.ThirdGame => thirdGame,
            _ => null
        };
    }

    private void Navigate(int direction)
    {
        int newState = ((int)currentState + direction + System.Enum.GetValues(typeof(GameState)).Length)
                        % System.Enum.GetValues(typeof(GameState)).Length;
        SwitchToState((GameState)newState);
    }

    private void RotateObject(GameState targetState)
    {
        int angleDifference = ((int)targetState - (int)currentState) * -90;
        if (miniGamesController != null)
        {
            miniGamesController.transform.Rotate(0, angleDifference, 0);
        }
    }

    public void DeactivateGame(bool interaction)
    {
        interactionsEnabled = interaction;
        if (interaction == true)
        {
            mainMenu.gameObject.SetActive(true);
        }
        else
        {
            mainMenu.gameObject.SetActive(false);
        }
    }

}
