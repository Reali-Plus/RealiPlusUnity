using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    [SerializeField] private Rigidbody[] keepAwake;

    private void Awake()
    {
        foreach (var rb in keepAwake)
        {
            rb.sleepThreshold = 0f;
        }
    }
}
