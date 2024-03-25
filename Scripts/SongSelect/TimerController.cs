using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerController : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    float currentValue;
    bool enableTimer = false;
    public GameObject[] componentsToHide;
    public AudioManager audioManager;

    void Start()
    {
        currentValue = 4f;
        timerText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (enableTimer)
        {
            currentValue -= 1 * Time.deltaTime;
            timerText.text = currentValue.ToString("0");

            if (currentValue <= 0)
            {
                currentValue = 0;
                enableTimer = false;
                HideTimer();
                audioManager.PlayAudio();
            }
        }
    }

    public void OnStartButtonClick()
    {
        enableTimer = true;
        ShowTimer();
        HideComponents();
    }

    void ShowTimer()
    {
        timerText.gameObject.SetActive(true);
    }

    void HideTimer()
    {
        timerText.gameObject.SetActive(false);
    }
    void HideComponents()
    {
        foreach (GameObject component in componentsToHide)
        {
            component.SetActive(false);
        }
    }
}
