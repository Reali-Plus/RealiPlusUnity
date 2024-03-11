using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class PhysicsController : MonoBehaviour
{
    new private Rigidbody rigidbody;
    private List<PhysicsController> nextControllers;

    private Quaternion rotationOffset;
    private Vector4 positionOffset;
    private Vector3 scaleOffset;
    private bool isInit = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        nextControllers = FindNextController(transform);
    }

    private void Init(PhysicsController parent)
    {
        if (isInit)
        {
            Debug.LogWarning($"{this} cannot be initialized more than once.");
            return;
        }

        if (parent == null)
        {
            positionOffset = transform.position;
            rotationOffset = transform.rotation;
            scaleOffset = transform.lossyScale;
        }
        else
        {
            positionOffset = parent.transform.InverseTransformPoint(transform.position);
            rotationOffset = Quaternion.Inverse(parent.transform.rotation) * transform.rotation;
            scaleOffset = Vector3.Scale(transform.lossyScale, parent.transform.lossyScale.Invert());
        }

        positionOffset.w = 1;
        isInit = true;
    }

    public void UpdateJointsHierarchy() => ApplyTransform(Matrix4x4.identity, Quaternion.identity);

    private void ApplyTransform(Matrix4x4 parentTRS, Quaternion parentRot)
    {
        if (!isInit)
        {
            Init(null);
        }

        parentTRS *= Matrix4x4.TRS(positionOffset, rotationOffset, scaleOffset);
        parentRot *= rotationOffset;

        ApplySelfTransform(ref parentTRS, ref parentRot);

        rigidbody.MovePosition(parentTRS * new Vector4(0, 0, 0, 1));
        rigidbody.MoveRotation(parentRot);

        foreach (var joint in nextControllers)
        {
            joint.ApplyTransform(parentTRS, parentRot);
        }
    }

    protected abstract void ApplySelfTransform(ref Matrix4x4 globalTRS, ref Quaternion globalRot);

    //protected abstract void ComputeJacobian();

    private List<PhysicsController> FindNextController(Transform root)
    {
        List<PhysicsController> joints = new List<PhysicsController>();
        Transform child;

        for (int i = 0; i < root.childCount; ++i)
        {
            child = root.GetChild(i);
            if (child.TryGetComponent(out PhysicsController joint))
            {
                joints.Add(joint);
                joint.Init(this);
            }
            else
            {
                joints.AddRange(FindNextController(child));
            }
        }

        return joints;
    }
}
