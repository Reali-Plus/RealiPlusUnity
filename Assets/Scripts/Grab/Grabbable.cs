using UnityEngine;

public class Grabbable : MonoBehaviour
{
    [SerializeField]
    private GrabListener grabListener;

    [SerializeField]
    private LayerMask grabbedObjectLayer;
    [SerializeField]
    private LayerMask defaultLayer;

    private bool isGrabbed = false;
    private Vector3 grabPositionOffset = Vector3.zero;
    private Quaternion grabRotationOffset = Quaternion.identity;
    private Transform grabParent = null;
    private Rigidbody rb;

    private float positionThreshold = 0f;
    private float rotationThreshold = 0f;

    /*private Vector3 targetPosition = Vector3.zero;
    private Quaternion targetRotation = Quaternion.identity;*/

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        grabListener = GameObject.FindGameObjectWithTag("GrabListener").GetComponent<GrabListener>();
    }

/*    private void Update()
    {
        if (isGrabbed)
        {
            targetPosition = grabParent.position + transform.TransformDirection(grabDirection);
            targetRotation = grabParent.rotation * grabRotationOffset;
        }
    }*/

    void FixedUpdate()
    {
        if (isGrabbed)
        {
            Vector3 targetPosition = grabParent.position + transform.TransformDirection(grabPositionOffset);
            Quaternion targetRotation = grabParent.rotation * Quaternion.Inverse(grabRotationOffset);

            // Smooth position
            // Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, Time.fixedDeltaTime * 5f);
            // Quaternion smoothedRotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 5f);

            if (Vector3.Distance(transform.position, targetPosition) > positionThreshold)
            {
                rb.MovePosition(targetPosition);
                rb.velocity = Vector3.zero;
            }

            if (Quaternion.Angle(transform.rotation, targetRotation) > rotationThreshold)
            {
                rb.MoveRotation(targetRotation);
                rb.angularVelocity = Vector3.zero;
            }
        }
    }


    public void Grab(Transform grabParent)
    {
        isGrabbed = true;
        this.grabParent = grabParent;
        grabPositionOffset = transform.InverseTransformDirection(transform.position - grabParent.position);
        grabRotationOffset = Quaternion.Inverse(transform.rotation) * grabParent.rotation;
        gameObject.layer = LayerMask.NameToLayer("GrabbedObject");
        //rb.isKinematic = true;
    }

    public void Release()
    {
        if (isGrabbed)
        {
            //rb.isKinematic = false;
            isGrabbed = false;
            grabListener.Release();
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
}
