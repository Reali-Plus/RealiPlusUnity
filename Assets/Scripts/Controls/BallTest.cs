using UnityEngine;
using UnityEngine.InputSystem;

public class BallTest : MonoBehaviour
{
    [SerializeField]
    private float minXValue;
    
    [SerializeField]
    private float maxXValue;

    public void ControlBall(InputAction.CallbackContext input)
    {
        var readValue = input.ReadValue<Vector2>();
        Debug.Log("readValue " + readValue);

        transform.position = new Vector3((readValue.x + 1) / 2 * (maxXValue - minXValue) + minXValue, 
                                         transform.position.y, 
                                         transform.position.z);
    }
}
