using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsJoint : MonoBehaviour
{
    [SerializeField] private JointEvent jointEvent;
    [SerializeField] private float minRotation;
    [SerializeField] private float maxRotation;

    new private Rigidbody rigidbody;
    private float currentAngle;
    private List<PhysicsJoint> nextJoints;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        nextJoints = FindNextJoints(transform);
    }

    public void UpdateJointsHierarchy() => ApplyTransform(Matrix4x4.Translate(transform.position), Quaternion.identity);

    private void ApplyTransform(Matrix4x4 parentTRS, Quaternion parentRot)
    {
        float targetAngle = Mathf.Lerp(minRotation, maxRotation, jointEvent.CurrentValue);
        Quaternion relRot = Quaternion.AngleAxis(targetAngle - currentAngle, transform.right);

        Matrix4x4 globalTRS = parentTRS * Matrix4x4.Rotate(relRot);
        Quaternion globalRot = relRot * parentRot;

        rigidbody.MovePosition(parentTRS * new Vector4(0, 0, 0, 1));
        rigidbody.MoveRotation(globalRot * transform.rotation);

        foreach (var joint in nextJoints)
        {
            Vector3 offset = joint.transform.position - transform.position;
            joint.ApplyTransform(globalTRS * Matrix4x4.Translate(offset), globalRot);
        }

        currentAngle = targetAngle;
    }

    private List<PhysicsJoint> FindNextJoints(Transform root)
    {
        List<PhysicsJoint> joints = new List<PhysicsJoint>();
        Transform child;

        for (int i = 0; i < root.childCount; ++i)
        {
            child = root.GetChild(i);
            if (child.TryGetComponent(out PhysicsJoint joint))
            {
                joints.Add(joint);
            }
            else
            {
                joints.AddRange(FindNextJoints(child));
            }
        }

        return joints;
    }
}
