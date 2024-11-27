using UnityEngine;

public class GrabbableBall : Grabbable
{
    private Rigidbody ballRigidbody;

    [SerializeField]
    private float launchForce = 10f;
    [SerializeField]
    private Transform hoop;
    [SerializeField]
    private Transform initialPosition;
    [SerializeField]
    private Collider hoopCollider;


    private bool ballLaunched = false;

    public override void Initialize() {  }
    
    private void Start()
    {
        ballRigidbody = GetComponent<Rigidbody>();
        ballRigidbody.useGravity = false;
        ballRigidbody.position = initialPosition.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !ballLaunched)
        {
            LaunchBall();
        }
    }

    public override void Release()
    {
        base.Release();
        if (!ballLaunched)
        {
            LaunchBall();
        }
    }

    private void LaunchBall()
    {
        if (ballRigidbody != null && hoop != null)
        {
            ballRigidbody.useGravity = true;

            Vector3 launchDirection = (hoop.position - ballRigidbody.position).normalized;

            ballRigidbody.AddForce(launchDirection * launchForce, ForceMode.Impulse);

            ballLaunched = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == hoopCollider)
        {
            StartCoroutine(ResetBall());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(ResetBall());
    }

    System.Collections.IEnumerator ResetBall()
    {
        yield return new WaitForSeconds(0.5f);
        ResetGrabbableBall();
    }

    public void ResetGrabbableBall()
    {
        ballRigidbody.velocity = Vector3.zero;
        ballRigidbody.angularVelocity = Vector3.zero;
        ballRigidbody.useGravity = false;
        ballRigidbody.position = initialPosition.position;
        ballLaunched = false;
    }
}
