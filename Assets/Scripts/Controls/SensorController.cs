using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorController : MonoBehaviour
{
    private SleeveData sleeveData;
    private SocketCommunication socketCommunication;

    void Start()
    {
        sleeveData = new SleeveData();
        socketCommunication = GameObject.FindGameObjectWithTag("SleeveCommunication").GetComponent<SocketCommunication>();
    }

    void Update()
    {
        if(socketCommunication.ReceiveData())
        {
            sleeveData = socketCommunication.GetData();
            Debug.Log(sleeveData);

        }
    }

    private void FixedUpdate()
    {
        transform.rotation *= Quaternion.Euler(new Vector3(sleeveData.Gyroscope.X, sleeveData.Gyroscope.Y, sleeveData.Gyroscope.Z) * Time.fixedDeltaTime);
        transform.position += new Vector3(sleeveData.Accelerometer.X, sleeveData.Accelerometer.Y, sleeveData.Accelerometer.Z) * Time.fixedDeltaTime;
    }
}
