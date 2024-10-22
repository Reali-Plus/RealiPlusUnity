using UnityEngine;

public class SensorController : MonoBehaviour
{
    [SerializeField]
    private bool supportRotation = true;
    [SerializeField]
    private bool supportTranslation = false;

    [SerializeField]
    private SensorID sensorID;

    [Header("Gravity stabilizer")]
    [SerializeField, Range(0f, 1f)]
    private float stabilizerRate = 0.05f;

    private SleeveData sleeveData;
    private Vector3 currentAccel;
    private Vector3 currentSpeed = Vector3.zero;
    private float stabilizerError = 10f; // Arbitrary large but not too large initial value;

    private void Awake()
    {
        sleeveData = new SleeveData();
    }

    private void Update()
    {
        // Gyro.X -> [0,1] ???
        if (supportRotation)
        {
            // Apply rotation
            Quaternion gyroRot = Quaternion.Euler(new Vector3(sleeveData.Gyroscope.X, sleeveData.Gyroscope.Y, sleeveData.Gyroscope.Z) * Time.deltaTime);
            Quaternion gravRot = Quaternion.FromToRotation(currentAccel, transform.InverseTransformDirection(Vector3.down));
            transform.localRotation *= Quaternion.Slerp(gyroRot, gravRot, GetStabilizerFactor());
        }

        if (supportTranslation)
        {
            // Swap Y and Z for right hand rule
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

        Vector3 oldAccel = currentAccel;
        currentAccel = new Vector3(sleeveData.Accelerometer.X, sleeveData.Accelerometer.Y, sleeveData.Accelerometer.Z);
        stabilizerError = (1 - stabilizerRate) * stabilizerError + 
                           stabilizerRate * 100 * (oldAccel - currentAccel).sqrMagnitude; // 100:Amplify error by an arbitrary amount
    }

    public SensorID GetSensorID()
    {
        return sensorID;
    }

    private float GetStabilizerFactor()
    {
        float beta = 50; // TEMP -> Parameter?
        float magnitude = currentAccel.magnitude;
        float kTime = Mathf.Exp(-Mathf.Pow(stabilizerError, 2));
        float kNorm = Mathf.Exp(-Mathf.Pow(magnitude - 1, 2));
        float kAngle = 1f;//Vector3.Dot(accel, transform.InverseTransformDirection(Vector3.down)) / magnitude;
        float k = Mathf.Max(kAngle, 0) * kNorm * kTime;
        return Mathf.Exp(-beta * Mathf.Pow(k - 1, 2));
    }
}