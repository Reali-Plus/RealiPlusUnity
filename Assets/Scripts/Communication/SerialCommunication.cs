using System.Collections;
using System.IO.Ports;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SerialCommunication : MonoBehaviour
{
    [SerializeField]
    private string portName = "COM3";
    [SerializeField]
    private int baudRate = 115200;
    private SerialPort serialPort;

    void Start()
    {
        serialPort = new SerialPort(portName, baudRate);
        try
        {
            serialPort.Open();
            serialPort.ReadTimeout = 1000;
        }
        catch (Exception e)
        {
            Debug.LogError("Error opening serial port: " + e.Message);
        }
    }

    void Update()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                string serialInput = serialPort.ReadLine();
                Debug.Log("Received: " + serialInput);
            }
            catch (TimeoutException)
            {
                Debug.LogWarning("Serial read timeout.");
            }
        }
    }

    private void OnApplicationQuit()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
