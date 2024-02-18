using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class SocketCommunication : MonoBehaviour
{
    private UdpClient udpClient;
    private const int port = 8000;

    private System.Diagnostics.Process process;
    private string lastReceivedMessage = "";

    private float roll = 0f;
    private float pitch = 0f;
    private float yaw = 0f;


    void Start()
    {
        udpClient = new UdpClient(port);

        DirectoryInfo dir = Directory.GetParent(Application.dataPath);
        string daemonPath = Path.Combine(dir.FullName, "Assets", "Scripts", "Communication", "communication-daemon-socket.py");

        RunProcess(daemonPath);
    }

    void RunProcess(string daemonPath)
    {
        process = new System.Diagnostics.Process();

        process.StartInfo.FileName = "python";
        process.StartInfo.Arguments = daemonPath;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        process.Start();
    }   

    void Update()
    {
        while (udpClient.Available > 0) 
        {
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, port);
            byte[] receivedBytes = udpClient.Receive(ref remoteEndPoint);
            string receivedString = Encoding.UTF8.GetString(receivedBytes);

            if (receivedString != lastReceivedMessage)
            {
                lastReceivedMessage = receivedString;
                ConvertMessage(receivedString);
            }
        }
    }

    void FixedUpdate()
    {
        if (transform.rotation.eulerAngles.x == pitch && transform.rotation.eulerAngles.y == yaw && transform.rotation.eulerAngles.z == roll)
        {
            return;
        } 
     
        transform.rotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void OnApplicationQuit()
    {
        udpClient?.Close();
        if (process != null && !process.HasExited)
        {
            process.Kill();
        }
    }

    void ConvertMessage(string message)
    {
        string[] rotations = message.Split(" ");

        if (rotations.Length > 1)
        {
            roll = ParseRotation(rotations[0]);
            pitch = ParseRotation(rotations[1]);
            yaw = ParseRotation(rotations[2]);

            Debug.Log("roll " + roll + "pitch " + pitch + "yaw " + yaw);
        }
    }

    float ParseRotation(string strRotation)
    {
        return float.TryParse(strRotation, out float rotation) ? rotation * Mathf.Rad2Deg : 0f;
    }
}