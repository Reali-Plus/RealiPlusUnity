using UnityEngine;

public class SleeveCommunication : MonoBehaviour
{
    private Communication communication;

    public enum CommunicationTypes { Serial, BLE };

    [SerializeField]
    private CommunicationTypes communicationType = CommunicationTypes.Serial;
    
    // TODO : - Add a menu to choose the communication type
    //        - Add a way to change communication type at runtime
    void Start()
    {
        if (communicationType == CommunicationTypes.Serial)
        {
            communication = new SerialCommunication();
        }
        else if (communicationType == CommunicationTypes.BLE)
        {
            communication = new SocketCommunication();
        }

        communication.Initialize();
    }

    private void OnApplicationQuit()
    {
        communication.OnExit();
    }

    public SleeveData GetData()
    {
        return communication.GetData();
    }

    public bool HasData()
    {
        return communication.HasData();
    }

    public void SendData(HapticsData hapticsData)
    {
        communication.SendData(hapticsData);
    }

    public bool ReceiveData()
    {
        return communication.ReceiveData();
    }
}
