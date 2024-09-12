using UnityEngine;

public class SleeveCommunication : MonoBehaviour
{
    private Communication communication;

    public enum CommunicationTypes { Serial, BLE };

    [SerializeField]
    private CommunicationTypes communicationType = CommunicationTypes.Serial;
    
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

    void Update()
    {
        communication.OnUpdate();
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

    public bool ReceiveData()
    {
        return communication.ReceiveData();
    }
}
