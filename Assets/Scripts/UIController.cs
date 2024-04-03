using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public WheelSpin wheelSpin;
    public Slider speedSlider;
    public Button startButton;
    public Button stopButton;
    void Start()
    {
        wheelSpin = FindObjectOfType<WheelSpin>();
        wheelSpin.enabled = false;

        startButton.onClick.AddListener(StartScene);
        stopButton.onClick.AddListener(StopScene);

        speedSlider.onValueChanged.AddListener(SetRotationSpeed);
    }

    void SetRotationSpeed(float speed)
    {
        wheelSpin.rotationSpeed = speed;
    }

    void StartScene()
    {
        wheelSpin.enabled = true;
    }

    void StopScene()
    {
        wheelSpin.enabled = false;
    }
}
