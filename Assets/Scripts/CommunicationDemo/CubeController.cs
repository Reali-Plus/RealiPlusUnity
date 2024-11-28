using UnityEngine;

public class CubeController : MonoBehaviour
{
    private SleeveData sleeveData;
    private SocketCommunication socketCommunication;

    private void Start()
    {
        sleeveData = new SleeveData();
        socketCommunication = GameObject.FindGameObjectWithTag("SleeveCommunication").GetComponent<SocketCommunication>();
    }

    private void Update()
    {
        if (socketCommunication.ReceiveData())
        {
            sleeveData = socketCommunication.GetDataToReceive();
        }
    }

    private void FixedUpdate()
    {
        transform.rotation *= Quaternion.Euler(new Vector3(sleeveData.Gyroscope.X, sleeveData.Gyroscope.Y, sleeveData.Gyroscope.Z) * Time.fixedDeltaTime);
    }
}