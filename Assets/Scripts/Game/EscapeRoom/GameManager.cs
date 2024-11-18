using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject firstGame;
    [SerializeField] private GameObject secondGame;
    //[SerializeField] private GameObject thirdGame;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject miniGamesController;

    private enum GameState { 
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
        currentState = GameState.Menu;
        UpdateGameState();
    }

    void Update()
    {
        //NUM NAV
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SwitchToState(GameState.Menu);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchToState(GameState.FirstGame);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchToState(GameState.SecondGame);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
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
        mainMenu.SetActive(currentState == GameState.Menu);
        firstGame.SetActive(currentState == GameState.FirstGame);
        secondGame.SetActive(currentState == GameState.SecondGame);
        //thirdGame.SetActive(currentState == GameState.ThirdGame);
    }

    private void SwitchToState(GameState targetState)
    {
        if (currentState != targetState)
        {
            RotateObject(targetState);
            currentState = targetState;
            UpdateGameState();
        }
    }

    private void Navigate(int direction)
    {
        // Change l'état en ajoutant ou soustrayant
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
}
