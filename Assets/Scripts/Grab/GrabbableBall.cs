using System.Collections;
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
    private float resetDelay = 5f;

    private Coroutine resetCoroutine;

    [SerializeField]
    private BallTrajectory ballTrajectory;

    private bool ballLaunched = false;

    private void Start()
    {
        ballRigidbody = GetComponent<Rigidbody>();
        ballRigidbody.useGravity = false;
        ballRigidbody.position = initialPosition.position;
    }

    private void OnDestroy()
    {
        StopResetCoroutine();
    }

    private void Update()
    {
        if (!ballLaunched)
            ballTrajectory.ShowTrajectoryLine(ballRigidbody.position, (hoop.position - ballRigidbody.position).normalized * launchForce / ballRigidbody.mass);
        
        if (!gameObject.activeInHierarchy)
            return;

        if (Input.GetMouseButtonDown(0) && !ballLaunched)
        {
            LaunchBall();
        }
    }

    public override void Grab(Transform grabParent)
    {
        base.Grab(grabParent);
        ballLaunched = false;
        StopResetCoroutine();
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
        if (ballRigidbody == null || hoop == null)
        {
            Debug.LogWarning($"Failed to launch ball, make sure all fields are set correctly.");
            return;
        }

        ballRigidbody.useGravity = true;

        Vector3 launchDirection = (hoop.position - ballRigidbody.position).normalized;

        ballRigidbody.AddForce(launchDirection * launchForce, ForceMode.Impulse);

        ballLaunched = true;

        resetCoroutine = StartCoroutine(ResetAfterDelay(resetDelay));
    }

    IEnumerator ResetAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        ResetGrabbableBall();
    }

    private void StopResetCoroutine()
    {
        if (resetCoroutine != null)
        {
            StopCoroutine(resetCoroutine);
            resetCoroutine = null;
        }
    }

    public void ResetGrabbableBall()
    {
        ballRigidbody.velocity = Vector3.zero;
        ballRigidbody.angularVelocity = Vector3.zero;
        ballRigidbody.useGravity = false;
        ballRigidbody.position = initialPosition.position;
        ballLaunched = false;

        StopResetCoroutine();
    }
}
