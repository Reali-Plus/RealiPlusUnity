using UnityEngine;

public class PendulumTest : MonoBehaviour
{
    public float initialAngle = 0f;

    void Start()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, initialAngle);
    }
}
