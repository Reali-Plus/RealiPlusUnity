using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentHelper : MonoBehaviour
{
    private struct TransformData
    {
        public Vector3 Position;
        public Quaternion Rotation;
    }

    private List<TransformData> childrenInitData = new();

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform t = transform.GetChild(i);
            childrenInitData.Add(new TransformData() { Position = t.position, Rotation = t.rotation });
        }
    }

    [ContextMenu("Reset children")]
    private void ResetChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform t = transform.GetChild(i);
            t.position = childrenInitData[i].Position;
            t.rotation = childrenInitData[i].Rotation;
        }
    }
}
