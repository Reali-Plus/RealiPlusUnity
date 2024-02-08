using UnityEngine;

public class RealiPlusController : MonoBehaviour
{
    [SerializeField] private PhysicsController root;

    private void FixedUpdate()
    {
        root.UpdateJointsHierarchy();
    }
}
