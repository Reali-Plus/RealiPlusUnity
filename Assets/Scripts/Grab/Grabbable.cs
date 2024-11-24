using UnityEngine;

public class Grabbable : MonoBehaviour
{
    private bool isGrabbed = false;
    private Vector3 grabPosition = Vector3.zero;
    private Vector3 grabDirection = Vector3.zero;
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
            rb.MovePosition(grabParent.position + transform.TransformDirection(grabDirection));
            rb.MoveRotation(grabParent.rotation * grabRotationOffset);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    public void UpdateTransform()
    {
        rb.MovePosition(grabParent.position + transform.TransformDirection(grabDirection));
        rb.MoveRotation(grabParent.rotation * grabRotationOffset);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Follow the grabParent with the same relative position
        // transform.position = grabParent.position + (transform.position - grabPosition);
        // Rotate the object to keep the same relative direction
        // transform.rotation = Quaternion.FromToRotation(grabDirection, transform.position - grabParent.position) * transform.rotation;
    }

    public void Grab(Transform grabParent)
    {
        isGrabbed = true;
        this.grabParent = grabParent;
        grabPosition = transform.position;
        grabDirection = transform.InverseTransformDirection(transform.position - grabParent.position);
        grabRotationOffset = transform.rotation * Quaternion.Inverse(grabParent.rotation);
    }

    public void Release()
    {
        isGrabbed = false;
    }
}
