using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    private SleeveData sleeveData;
    private SocketCommunication socketCommunication;

    void Start()
    {
        sleeveData = new SleeveData(0, 0, 0);
        socketCommunication = GameObject.FindGameObjectWithTag("SleeveCommunication").GetComponent<SocketCommunication>();
    }

    void Update()
    {
        SleeveData? data = socketCommunication.ReceiveData();
        if (data != null)
        {
            sleeveData = (SleeveData)data;
        }
    }

    private void FixedUpdate()
    {
        transform.rotation *= Quaternion.Euler(sleeveData.Pitch * Time.fixedDeltaTime, sleeveData.Yaw * Time.fixedDeltaTime, sleeveData.Roll * Time.fixedDeltaTime);
    }
}
