using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
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

    void Start()
    {
        Debug.Log("Starting socket communication");
        udpClient = new UdpClient(port);
        endPoint = new IPEndPoint(IPAddress.Loopback, port);

        DirectoryInfo dir = Directory.GetParent(Application.dataPath);
        string daemonPath = Path.Combine(dir.FullName, "Assets", "Scripts", "Communication", dameonFileName);

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

    public SleeveData ReceiveData()
    {
        SleeveData sleeveData = new SleeveData(0, 0, 0);
        if (udpClient == null || endPoint == null)
        {
            return sleeveData;
        }

        if (udpClient.Available > 0) // TODO: maybe change to if
        {
            byte[] receivedBytes = udpClient.Receive(ref endPoint);
            string receivedString = Encoding.UTF8.GetString(receivedBytes);

            sleeveData.FromString(receivedString);
        }
        return sleeveData;
    }

    public void SendData(HapticsData hapticsData)
    {
        byte[] data = hapticsData.ToBytes();
        if (udpClient != null && endPoint != null & data.Length > 0)
        {
            udpClient.Send(data, data.Length, endPoint);
        }
    }

    void OnApplicationQuit()
    {
        udpClient?.Close();
        if (process != null && !process.HasExited)
        {
            process.Kill();
        }
    }
}