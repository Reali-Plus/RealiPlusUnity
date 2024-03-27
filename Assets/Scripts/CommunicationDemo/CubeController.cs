using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    [SerializeField]
    private SleeveData sleeveData;
    [SerializeField]
    private SocketCommunication socketCommunication;

    void Start()
    {
        sleeveData = new SleeveData(0, 0, 0);
        socketCommunication = GameObject.FindGameObjectWithTag("SleeveCommunication").GetComponent<SocketCommunication>();
    }

    void Update()
    {
        socketCommunication.ReceiveData();
        if (socketCommunication.HasData())
        {
            sleeveData = socketCommunication.GetData();
            Debug.Log(sleeveData.ToString());
        }
        /*SleeveData? data = socketCommunication.ReceiveData();
        if (data != null)
        {
            sleeveData = (SleeveData)data;
        }*/
    }

    private void FixedUpdate()
    {
        transform.rotation *= Quaternion.Euler(sleeveData.Pitch * Time.fixedDeltaTime, sleeveData.Yaw * Time.fixedDeltaTime, sleeveData.Roll * Time.fixedDeltaTime);
    }
}
