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
    [SerializeField, Min(0.01f)]
    private float stabilizerFactor = 50f;

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
        if (supportRotation)
        {
            // Apply rotation
            Vector3 gyroRot = new Vector3(sleeveData.Gyroscope.X, sleeveData.Gyroscope.Y, sleeveData.Gyroscope.Z) * Time.deltaTime;
            Quaternion rawRot = Quaternion.Euler(gyroRot);
            Quaternion gravRot = Quaternion.Euler(Vector3.Scale(gyroRot, new Vector3(Mathf.Abs(currentAccel.x), Mathf.Abs(currentAccel.y), Mathf.Abs(currentAccel.z))));
            Quaternion stabilizerRot = gravRot * Quaternion.FromToRotation(currentAccel, transform.InverseTransformDirection(Vector3.down));
            transform.localRotation *= Quaternion.Slerp(rawRot, stabilizerRot, GetStabilizerFactor());
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
        float currentError = 100 * (oldAccel - currentAccel).sqrMagnitude + 
                             0.05f * Mathf.Abs(sleeveData.Gyroscope.X * sleeveData.Gyroscope.Y * sleeveData.Gyroscope.Z); // Amplify error by an arbitrary amount
        stabilizerError = (1 - stabilizerRate) * stabilizerError + stabilizerRate * currentError;
    }

    public SensorID GetSensorID()
    {
        return sensorID;
    }

    private float GetStabilizerFactor()
    {
        float beta = stabilizerFactor / Mathf.Clamp(stabilizerError, 0.1f, 1f); // TODO -> Parameter
        float kTime = Mathf.Exp(-Mathf.Pow(stabilizerError, 2));
        float kNorm = Mathf.Exp(-Mathf.Pow(currentAccel.sqrMagnitude - 1, 2));
        float k = kNorm * kTime;
        return Mathf.Exp(-beta * Mathf.Pow(k - 1, 2));
    }
}