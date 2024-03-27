using UnityEngine;

public class CubeController : MonoBehaviour
{
    private SleeveData sleeveData;
    private SocketCommunication socketCommunication;

    private void Start()
    {
        sleeveData = new SleeveData(0, 0, 0);
        socketCommunication = GameObject.FindGameObjectWithTag("SleeveCommunication").GetComponent<SocketCommunication>();
    }

    private void Update()
    {
        if (socketCommunication.ReceiveData())
        {
            sleeveData = socketCommunication.GetData();
        }
    }

    private void FixedUpdate()
    {
        transform.rotation *= Quaternion.Euler(sleeveData.Pitch * Time.fixedDeltaTime, sleeveData.Yaw * Time.fixedDeltaTime, sleeveData.Roll * Time.fixedDeltaTime);
    }
}
