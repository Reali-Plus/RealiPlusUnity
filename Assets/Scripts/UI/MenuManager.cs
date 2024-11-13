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

    [SerializeField]
    private TMPro.TMP_InputField portNameInput;
    [SerializeField]
    private TMPro.TMP_InputField baudRateInput;

    [SerializeField]
    private TMPro.TMP_Text warningText;

    private SleeveCommunication sleeveCommunication;

    private void Start()
    {
        sleeveCommunication = FindObjectOfType<SleeveCommunication>();
        warningText = warningMessage.GetComponentInChildren<TMPro.TMP_Text>();

        SerialCommunication.OnCommunicationError += ShowCommunicationError;

        ActivateWarning("Communication non initialisée");
        ShowMainMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void ShowCommunicationError(string message)
    {
        ActivateWarning(message);
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

        SerialCommunication serialCommunication = sleeveCommunication.GetSerialCommunication();
        portNameInput.text = serialCommunication.PortName;
        baudRateInput.text = serialCommunication.BaudRate.ToString();
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

    public void InitilializeOption()
    {
        SerialCommunication serialCommunication = sleeveCommunication.GetSerialCommunication();

        serialCommunication.PortName = portNameInput.text;
        serialCommunication.BaudRate = int.Parse(baudRateInput.text);

        DeactivateWarning();

        sleeveCommunication.InitilializeCommunication();
    }

    public void DeactivateWarning()
    {
        warningMessage.SetActive(false);
        StartButton.interactable = true;
    }

    public void ActivateWarning(string message)
    {
        warningText.text = message;
        warningMessage.SetActive(true);
        StartButton.interactable = false;
    }

    public void QuitOption()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

}
