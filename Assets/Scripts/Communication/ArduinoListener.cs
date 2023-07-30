using System;
using UnityEngine;

[RequireComponent(typeof(SerialController))]
public class ArduinoListener : MonoBehaviour
{
    public event Action<string> OnMessageReceived;

    private bool connected = false;

    // Called by SerialController, do not modify method name
    private void OnMessageArrived(string message)
    {
        OnMessageReceived?.Invoke(message);
    }

    // Called by SerialController, do not modify method name
    private void OnConnectionEvent(bool success)
    {
        if (success)
        {
            Debug.Log("Connection established.");
        }
        else
        {
            Debug.Log(connected ? "Connection lost" : "Failed to connect");
        }

        connected = success;
    }
}
