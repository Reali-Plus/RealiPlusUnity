using System.IO.Ports;
using UnityEngine;
using System;

public class SerialCommunication : Communication
{
    private string portName = "COM3";
    private int baudRate = 115200;
    private SerialPort serialPort;
    
    public override void Initialize()
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

    public override bool ReceiveData()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                string serialInput = serialPort.ReadLine();
                Debug.Log("Received: " + serialInput);

                return AddData(serialInput);
            }
            catch (TimeoutException)
            {
                Debug.LogWarning("Serial read timeout.");
            }
        }
        return false;
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
