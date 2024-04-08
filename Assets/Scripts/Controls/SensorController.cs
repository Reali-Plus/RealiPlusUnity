using UnityEngine;

public class SensorController : MonoBehaviour
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
            sleeveData = socketCommunication.GetData();
            Debug.Log(sleeveData);
        }
    }

    private void FixedUpdate()
    {
        if (sleeveData == null)
        {
            return;
        }
        transform.rotation *= Quaternion.Euler(new Vector3(sleeveData.Gyroscope.X, sleeveData.Gyroscope.Y, sleeveData.Gyroscope.Z) * Time.fixedDeltaTime);
        // TODO : add translation
        // transform.Translate(new Vector3(sleeveData.Accelerometer.X, sleeveData.Accelerometer.Y, sleeveData.Accelerometer.Z) * Time.fixedDeltaTime);
    }
}