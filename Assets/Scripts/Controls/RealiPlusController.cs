using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealiPlusController : MonoBehaviour
{
    [SerializeField] private PhysicsJoint root;

    private void FixedUpdate()
    {
        root.UpdateJointsHierarchy();
    }
}
