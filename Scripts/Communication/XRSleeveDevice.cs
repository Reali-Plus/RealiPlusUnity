using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum SleeveSide
{
    Default, Left, Right
}

public class XRSleeveDevice // TODO: Inherit from InputDevice
{
    public string DeviceName { get; private set; }
    public SleeveSide Side
    {
        get { return side; }
        set
        {
            if (side == value)
            {
                return;
            }

            side = value;
            OnConfigChanged?.Invoke(side);
        }
    }

    public event Action<object> OnDataReceived;
    public event Action<SleeveSide> OnConfigChanged;

    private SleeveSide side = SleeveSide.Default;

    public XRSleeveDevice(string communicationData)
    {
        // TODO: Initialize communication link
        DeviceName = communicationData;
    }

    public void SendHapticCommand(float lowFreq, float highFreq)
    {
        // TODO: Replace Gamepad with own protocol
        Gamepad.current?.SetMotorSpeeds(lowFreq, highFreq);
        Debug.Log($"Sending command ({lowFreq}, {highFreq}) to device [{Gamepad.current}]");
    }
}
