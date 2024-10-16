using UnityEngine;

public class SensorController : MonoBehaviour
{
    [SerializeField]
    private bool supportRotation = true;
    [SerializeField]
    private bool supportTranslation = false;

    [SerializeField]
    private SensorID sensorID;

    private SleeveData sleeveData;
    private Vector3 currentAccel;
    private Vector3 currentSpeed = Vector3.zero;

    private void Awake()
    {
        sleeveData = new SleeveData();
    }

    private void Update()
    {
        // Gyro.X -> [0,1] ???
        if (supportRotation)
        {
            // Remap axis and apply rotation
            transform.localRotation *= Quaternion.Euler(new Vector3(sleeveData.Gyroscope.Y, sleeveData.Gyroscope.Z, sleeveData.Gyroscope.X) * Time.deltaTime);
        }

        if (supportTranslation)
        {
            // Swap Y and Z for right hand rule
            currentAccel = new Vector3(sleeveData.Accelerometer.X, sleeveData.Accelerometer.Z, sleeveData.Accelerometer.Y);
            Debug.Log($"{gameObject} : {currentAccel}");
            Vector3 transformAccel = transform.TransformDirection(currentAccel);
            Debug.Log($"{gameObject} : {transformAccel}");
            currentSpeed += (transformAccel - Vector3.down) * 9.80665f * Time.deltaTime;
            transform.Translate(currentSpeed * Time.deltaTime);
        }
    }

    public void ReceiveData(SleeveData newData)
    {
        sleeveData = newData;
    }

    public SensorID GetSensorID()
    {
        return sensorID;
    }
}