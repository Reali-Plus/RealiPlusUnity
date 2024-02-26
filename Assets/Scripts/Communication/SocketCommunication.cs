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
    //private Vector3 acceleration;


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
        // transform.Translate(acceleration * Time.fixedDeltaTime);

        if (transform.rotation.eulerAngles.x == pitch && transform.rotation.eulerAngles.y == yaw && transform.rotation.eulerAngles.z == roll)
        {
            return;
        } 
        
        transform.rotation *= Quaternion.Euler(pitch * Time.fixedDeltaTime, yaw * Time.fixedDeltaTime, roll * Time.fixedDeltaTime);
        
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
        Debug.Log(message);
        string[] data = message.Split(" ");
           
        // TODO : Check if the message is valid
        if (data.Length >= 3)
        {
            roll = ParseRotation(data[0]);
            pitch = ParseRotation(data[1]);
            yaw = ParseRotation(data[2]);
            /*float accX = float.TryParse(data[0], out float acc) ? acc * 9.8f : 0f;
            float accY = float.TryParse(data[1], out acc) ? acc * 9.8f : 0f;
            float accZ = float.TryParse(data[2], out acc) ? (acc - 1) * 9.8f  : 0f;
            acceleration = new Vector3(accX, accY, accZ);*/

            //Debug.Log(acceleration);
            Debug.Log("roll " + roll + " pitch " + pitch + " yaw " + yaw);
        }
    }

    float ParseRotation(string strRotation)
    {
        return float.TryParse(strRotation, out float rotation) ? rotation : 0f;
    }
}