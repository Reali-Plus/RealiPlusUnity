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
        serialPort.ReadTimeout = 2; // ms
        try
        {
            serialPort.Open();
            serialPort.DiscardInBuffer();
            serialPort.DiscardOutBuffer();
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
                // Debug.Log("Sending data: " + hapticsData.ToString());
                serialPort.Write(hapticsData.ToString());
            }
            catch (TimeoutException)
            { }
        }
    }

    public override bool ReceiveData()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                string serialInput = serialPort.ReadLine(); // Blocks execution if there's nothing to read in serial port
                return AddData(serialInput);
            }
            catch (TimeoutException) { }
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
