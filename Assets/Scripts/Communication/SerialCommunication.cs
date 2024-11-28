using System.IO.Ports;
using UnityEngine;
using System;

public class SerialCommunication : Communication
{
    public string PortName { get; set; } = "COM8";
    public int BaudRate { get; set; } = 115200;
    private SerialPort serialPort;

    public static event Action<string> OnCommunicationError;


    public override void Close()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }

    public override void Initialize()
    {
        serialPort = new SerialPort(PortName, BaudRate);
        serialPort.ReadTimeout = 2; // ms
        try
        {
            serialPort.Open();
            serialPort.DiscardInBuffer();
            serialPort.DiscardOutBuffer();
        }
        catch (Exception e)
        {
            Debug.LogWarning("Error opening serial port: " + e.Message);
            OnCommunicationError?.Invoke("Le port " + PortName + " n'existe pas.");
        }
    }

    public override void SendData(HapticsData hapticsData)
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Write(hapticsData.ToString());
        }
    }

    public override bool ReceiveData()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                string serialInput = serialPort.ReadLine(); // Blocks execution if there's nothing to read in serial port
                return AddDataToReceive(serialInput);
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

    public override bool TestCommunication()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.WriteLine("Test");
            string serialInput = serialPort.ReadLine();
            return serialInput == "Test";
        }

        return false;
    }
}
