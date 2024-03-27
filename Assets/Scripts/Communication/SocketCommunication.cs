using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

// TODO: make this class a singleton
public class SocketCommunication : MonoBehaviour
{
    [SerializeField]
    private int port = 8000;
    [SerializeField]
    private string dameonFileName = "communication-daemon.py";

    private UdpClient udpClient;
    private IPEndPoint endPoint;

    private System.Diagnostics.Process process;

    private Queue<SleeveData> dataQueue;

    private void Start()
    {
        Debug.Log("Starting socket communication");
        udpClient = new UdpClient(port);
        endPoint = new IPEndPoint(IPAddress.Loopback, port);

        DirectoryInfo dir = Directory.GetParent(Application.dataPath);
        string daemonPath = Path.Combine(dir.FullName, "Assets", "Scripts", "Communication", dameonFileName);

        dataQueue = new Queue<SleeveData>();
        // RunProcess(daemonPath);
    }

    private void RunProcess(string daemonPath)
    {
        process = new System.Diagnostics.Process();

        process.StartInfo.FileName = "python";
        process.StartInfo.Arguments = daemonPath;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        process.Start();
    }   

    public SleeveData GetData()
    {
        if (dataQueue.Count > 0)
        {
            return dataQueue.Dequeue();
        }
        return new SleeveData(0, 0, 0);
    }

    public bool HasData()
    {
        return dataQueue.Count > 0;
    }

    public void ReceiveData()
    {
        SleeveData sleeveData = new SleeveData(0, 0, 0);
        if (udpClient == null || endPoint == null)
        {
            return;
        }

        if (udpClient.Available > 0)
        {
            byte[] receivedBytes = udpClient.Receive(ref endPoint);
            string receivedString = Encoding.UTF8.GetString(receivedBytes);
            Debug.Log("Received string : " + receivedString);
            if(ValidateResponse(receivedString))
            {
                sleeveData.FromString(receivedString);
                Debug.Log("Received data : " + sleeveData.ToString());
                dataQueue.Enqueue(sleeveData);
            }
        }
    }

    public void SendData(HapticsData hapticsData)
    {
        byte[] data = hapticsData.ToBytes();
        if (udpClient != null && endPoint != null & data.Length > 0)
        {
            udpClient.Send(data, data.Length, endPoint);
        }
    }

    private void OnApplicationQuit()
    {
        udpClient?.Close();
        if (process != null && !process.HasExited)
        {
            process.Kill();
        }
    }

    private bool ValidateResponse(string response)
    {
        bool isValid = true;
        string[] data = response.Split("|");
        if (data.Length == 2)
        {
            if (data[0].Equals("4")) // Daemon disconnected from sleeve
            {
                isValid = false;
            }
            Debug.Log("Response " + data[1]);
        }

        return isValid;
    }


}