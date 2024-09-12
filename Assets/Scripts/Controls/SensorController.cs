using UnityEngine;

public class SensorController : MonoBehaviour
{
    [SerializeField]
    private bool supportRotation = true;
    [SerializeField]
    private bool supportTranslation = false;

    private SleeveData sleeveData;
    private SleeveCommunication sleeveCommunication;

    private void Start()
    {
        sleeveData = new SleeveData();
        sleeveCommunication = GameObject.FindGameObjectWithTag("SleeveCommunication").GetComponent<SleeveCommunication>();
    }

    private void Update()
    {
        if (sleeveCommunication.ReceiveData())
        {
            sleeveData = sleeveCommunication.GetData();
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