using System;
using UnityEngine;

[Obsolete("PhysicsController is obsolete; this class serves no purpose.")]
public class RealiPlusController : MonoBehaviour
{
    [SerializeField] private PhysicsController root;

    private void FixedUpdate()
    {
        root.UpdateJointsHierarchy();
    }
}
