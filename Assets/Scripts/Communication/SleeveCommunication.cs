using Assets.Scripts.Communication;
using System.Collections.Generic;
using UnityEngine;

public class SleeveCommunication : MonoBehaviour
{
    private Communication communication;
    private Dictionary<FingerUtils.Finger, SensorController> sensors;

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

        sensors = new Dictionary<FingerUtils.Finger, SensorController>();
        List<SensorController> sensorControllers = new List<SensorController>(FindObjectsOfType<SensorController>());
        for (int i = 0; i < sensorControllers.Count; ++i)
        {
            sensors.Add(sensorControllers[i].GetFingerID(), sensorControllers[i]);
        }

        communication.Initialize();
    }

    private void Update()
    {
        if (communication != null && communication.ReceiveData())
        {
            SleeveData data = communication.GetData();
            Debug.Log(data.ToString());
            if (sensors.ContainsKey(data.FingerID))
            {
                sensors[data.FingerID].ReceiveData(data);
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
}
