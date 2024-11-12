using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;

    [SerializeField]
    private GameObject mainMenu;

    [SerializeField]
    private GameObject communicationMenu;

    [SerializeField]
    private GameObject warningMessage;

    [SerializeField]
    private Button StartButton;

    private SleeveCommunication sleeveCommunication;

    private void Start()
    {
        sleeveCommunication = FindObjectOfType<SleeveCommunication>();
        ShowMainMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }

        if (sleeveCommunication.IsInitialized)
        {
            warningMessage.SetActive(false);
            StartButton.interactable = true;
        }
        else
        {
            warningMessage.SetActive(true);
            StartButton.interactable = false;
        }
    }

    public void ShowWarningMessage()
    {
        warningMessage.SetActive(true);
    }

    public void HideWarningMessage()
    {
        warningMessage.SetActive(false);
    }

    public void ToggleMenu()
    {
        if (menu.activeSelf)
        {
            HideMenu();
        }
        else
        {
            ShowMenu();
        }
    }

    public void ShowMenu()
    {
        menu.SetActive(true);
        ShowInitialMenu();
    }

    public void HideMenu()
    {
        menu.SetActive(false);
    }

    public void ShowCommunicationMenu()
    {
        mainMenu.SetActive(false);
        communicationMenu.SetActive(true);
    }

    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        communicationMenu.SetActive(false);
    }

    public void ShowInitialMenu()
    {
        ShowMainMenu();
    }

    public void StartOption()
    {
        // Call start game on game manager
    }

    public void QuitOption()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

}
