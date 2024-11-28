using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

public class SocketCommunication : Communication
{
    private int port = 8000;
    private string dameonFileName = "communication-daemon.py";

    private UdpClient udpClient;
    private IPEndPoint endPoint;

    private System.Diagnostics.Process process;

    public override void Close()
    {
        udpClient?.Close();
        if (process != null && !process.HasExited)
        {
            process.Kill();
        }
    }

    public override void Initialize()
    {
        Debug.Log("Starting socket communication");
        udpClient = new UdpClient(port);
        endPoint = new IPEndPoint(IPAddress.Loopback, port);

        DirectoryInfo dir = Directory.GetParent(Application.dataPath);
        string daemonPath = Path.Combine(dir.FullName, "Assets", "Scripts", "Communication", dameonFileName);

        RunProcess(daemonPath);
    }

    // TODO: add validation for the daemon path and an excutable for the daemon (mode debug vs mode release)
    private void RunProcess(string daemonPath)
    {
        process = new System.Diagnostics.Process();

        process.StartInfo.FileName = "python";
        process.StartInfo.Arguments = daemonPath;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        process.Start();
    }

    public override bool ReceiveData()
    {
        if (udpClient == null || endPoint == null)
        {
            return false;
        }

        if (udpClient.Available > 0)
        {
            try
            {
                byte[] receivedBytes = udpClient.Receive(ref endPoint);
                string receivedString = Encoding.UTF8.GetString(receivedBytes);

                if (receivedString != null && ValidateResponse(receivedString))
                {
                    return AddDataToReceive(receivedString);
                }
            }
            catch (SocketException e)
            {
                Debug.LogError("Socket exception: " + e.Message);
            }
        }

        return false;
    }

    public override void SendData(HapticsData hapticsData)
    {
        if (udpClient != null && endPoint != null)
        {
            byte[] data = Encoding.UTF8.GetBytes(hapticsData.ToString());

            if (data.Length > 0)
            {
                udpClient.Send(data, data.Length, endPoint);
            }
        }
    }

    public override void OnExit()
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
            Debug.Log("Daemon Message: " + data[1]);
        }

        return isValid;
    }

    public override bool TestCommunication()
    {
        return udpClient != null && endPoint != null;
    }
}