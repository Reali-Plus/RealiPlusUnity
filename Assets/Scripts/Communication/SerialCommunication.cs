using System.IO.Ports;
using UnityEngine;
using System;

public class SerialCommunication : Communication
{
    // TODO : List available ports and baud rates to choose from a menu
    private string portName = "COM8";
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

    public override void SendData(HapticsData hapticsData)
    {
        if (serialPort != null && serialPort.IsOpen)
        {   
            try
            {
                serialPort.WriteLine(hapticsData.ToString());
            }
            catch (TimeoutException)
            {
                Debug.LogWarning("Serial write timeout.");
            }
        }
    }

    public override bool ReceiveData()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                string serialInput = serialPort.ReadLine();
                return AddData(serialInput);
            }
            catch (TimeoutException)
            {
                Debug.LogWarning("Serial read timeout.");
            }
        }
        return false;
    }

    public override void OnExit()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}