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
    private bool testMode = true;

    [SerializeField]
    private CommunicationMode communicationMode = CommunicationMode.Serial;
    
    private bool isInitialized = false;

    // TODO : - Add a menu to choose the communication type
    //        - Add a way to change communication type at runtime
    void Start()
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
        if (isInitialized)
        {
            communication.TestCommunication();
        }
        else
        {
            InitilializeCommunication();
        }
    }

    private void InitilializeCommunication()
    {         
        sensors = new Dictionary<SensorID, SensorController>();
        List<SensorController> sensorControllers = new List<SensorController>(FindObjectsOfType<SensorController>());
        for (int i = 0; i < sensorControllers.Count; ++i)
        {
            sensors.Add(sensorControllers[i].GetSensorID(), sensorControllers[i]);
        }

        communication.Initialize();
        isInitialized = true;
    }

    private void Update()
    {
        while (communication != null && communication.ReceiveData())
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
        communication.SendData(new HapticsData(SensorID.Calibrate, true, true));
    }
}
