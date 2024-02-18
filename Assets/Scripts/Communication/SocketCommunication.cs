using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class SocketCommunication : MonoBehaviour
{
    private UdpClient udpClient;
    private const int port = 8000;

    private string lastReceivedMessage = "";

    private float roll = 0f;
    private float pitch = 0f;
    private float yaw = 0f;


    void Start()
    {
        udpClient = new UdpClient(port);
    }

    void Update()
    {
        while (udpClient.Available > 0) 
        {
            // Debug.Log("Available: " + udpClient.Available);
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, port);
            byte[] receivedBytes = udpClient.Receive(ref remoteEndPoint);
            string receivedString = Encoding.UTF8.GetString(receivedBytes);
            // Debug.Log("Received: " + receivedString);

            if (receivedString != lastReceivedMessage)
            {
                lastReceivedMessage = receivedString;
                ConvertMessage(receivedString);
            }
        }
    }

    void FixedUpdate()
    {
        // TODO : only update if the rotation has changed
        if (transform.rotation.eulerAngles.x == pitch && transform.rotation.eulerAngles.y == yaw && transform.rotation.eulerAngles.z == roll)
        {
            return;
        } 
     
        transform.rotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void OnApplicationQuit()
    {
        if (udpClient != null)
        {
            udpClient.Close();
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

            UnityEngine.Debug.Log("roll " + roll);
            UnityEngine.Debug.Log("pitch " + pitch);
            UnityEngine.Debug.Log("yaw " + yaw);
        }
    }

    float ParseRotation(string strRotation)
    {
        return float.TryParse(strRotation, out float rotation) ? rotation * Mathf.Rad2Deg : 0f;
    }
}