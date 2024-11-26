using UnityEngine;

public class Grabbable : MonoBehaviour
{
    [SerializeField]
    private LayerMask grabbedObjectLayer;
    [SerializeField]
    private LayerMask defaultLayer;

    private bool isGrabbed = false;
    private Vector3 grabPositionOffset = Vector3.zero;
    private Quaternion grabRotationOffset = Quaternion.identity;
    private Transform grabParent = null;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isGrabbed)
        {
            Vector3 targetPosition = grabParent.position + transform.TransformDirection(grabPositionOffset);
            Quaternion targetRotation = grabParent.rotation * Quaternion.Inverse(grabRotationOffset);
            
            rb.MovePosition(targetPosition);
            rb.velocity = Vector3.zero;
            
            rb.MoveRotation(targetRotation);
            rb.angularVelocity = Vector3.zero;
        }
    }


    public void Grab(Transform grabParent)
    {
        isGrabbed = true;
        this.grabParent = grabParent;
        grabPositionOffset = transform.InverseTransformDirection(transform.position - grabParent.position);
        grabRotationOffset = Quaternion.Inverse(transform.rotation) * grabParent.rotation;
        gameObject.layer = LayerMask.NameToLayer("GrabbedObject");
    }

    public void Release()
    {
        if (isGrabbed)
        {
            isGrabbed = false;
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
}
