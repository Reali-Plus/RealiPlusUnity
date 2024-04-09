using UnityEngine;

public class SensorController : MonoBehaviour
{
    [SerializeField]
    private bool supportRotation = false;
    [SerializeField]
    private bool supportTranslation = false;

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

        if (supportRotation)
        {
            transform.rotation *= Quaternion.Euler(new Vector3(sleeveData.Gyroscope.X, sleeveData.Gyroscope.Y, sleeveData.Gyroscope.Z) * Time.fixedDeltaTime);
        }

        if (supportTranslation)
        {
            transform.Translate(new Vector3(sleeveData.Accelerometer.X, sleeveData.Accelerometer.Y, sleeveData.Accelerometer.Z) * Time.fixedDeltaTime);
        }
    }
}