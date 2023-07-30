using UnityEngine;

public class BallTest : MonoBehaviour
{
    [SerializeField] 
    private ArduinoListener arduinoListener;

    [SerializeField]
    private int minSerialValue;
    
    [SerializeField]
    private int maxSerialValue;

    [SerializeField]
    private float minXValue;
    
    [SerializeField]
    private float maxXValue;

    private void OnEnable()
    {
        arduinoListener.OnMessageReceived += ControlBall;
    }

    private void OnDisable()
    {
        arduinoListener.OnMessageReceived -= ControlBall;
    }

    private void ControlBall(string msg)
    {
        float serialValue = int.Parse(msg);
        transform.position = new Vector3((serialValue - minSerialValue) / maxSerialValue * (maxXValue - minXValue) + minXValue, 
                                         transform.position.y, 
                                         transform.position.z);
    }
}
