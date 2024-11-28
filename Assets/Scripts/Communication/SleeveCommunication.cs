using System.Collections.Generic;
using UnityEngine;

public class SleeveCommunication : MonoBehaviour
{
    public static SleeveCommunication Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject().AddComponent<SleeveCommunication>();
                instance.name = instance.GetType().ToString();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }
    private static SleeveCommunication instance;

    private Communication communication;
    private Dictionary<SensorID, SensorController> sensors;

    public enum CommunicationMode { Serial, BLE };

    [SerializeField]
    private bool initializeOnStart = true;

    [SerializeField]
    private CommunicationMode communicationMode = CommunicationMode.Serial;

    public bool IsInitialized { get; set; }

    void Awake()
    {
        if (communicationMode == CommunicationMode.Serial)
        {
            communication = new SerialCommunication();
        }
        else if (communicationMode == CommunicationMode.BLE)
        {
            communication = new SocketCommunication();
        }

        if (initializeOnStart)
        {
            InitilializeCommunication();
        }
    }

    public void TestCommunication()
    {
        if (IsInitialized)
        {
            communication.TestCommunication();
        }
        else
        {
            InitilializeCommunication();
        }
    }

    public void InitilializeCommunication()
    {         
        if (IsInitialized)
            communication.Close();

        sensors = new Dictionary<SensorID, SensorController>();
        List<SensorController> sensorControllers = new List<SensorController>(FindObjectsOfType<SensorController>());
        for (int i = 0; i < sensorControllers.Count; ++i)
        {
            sensors.Add(sensorControllers[i].GetSensorID(), sensorControllers[i]);
        }

        communication.Initialize();
        IsInitialized = true;
    }

    private void Update()
    {
        while (communication != null && IsInitialized && communication.ReceiveData())
        {
            SleeveData data = communication.GetData();
            if (sensors.ContainsKey(data.SensorID))
            {
                sensors[data.SensorID].ReceiveData(data);
            }
        }
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

    public void CalibrateSleeve()
    {
        if (IsInitialized)
        {
            communication.SendData(new HapticsData(SensorID.Calibrate, true, true));
        }
        else
        {
            Debug.LogWarning("Tried to calibrate, communication not initialized");
        }
    }

    public SerialCommunication GetSerialCommunication()
    {
        return communication as SerialCommunication;
    }
}
