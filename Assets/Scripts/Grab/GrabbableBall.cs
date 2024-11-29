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

    private bool isLaunched = false;

    public bool IsLaunched() => isLaunched;

    private void Start()
    {
        ballRigidbody = GetComponent<Rigidbody>();
        ballRigidbody.position = initialPosition.position;
    }

    private void OnDestroy()
    {
        StopResetCoroutine();
    }

    private void Update()
    {
        if (!gameObject.activeInHierarchy)
            return;

        if (!isLaunched)
            ballTrajectory.ShowTrajectoryLine(ballRigidbody.position, (hoop.position - ballRigidbody.position).normalized * launchForce / ballRigidbody.mass);
        else
            ballTrajectory.HideLine();
    }

    public override void Grab(Transform grabParent)
    {
        base.Grab(grabParent);
        isLaunched = false;
        StopResetCoroutine();
    }

    public override void Release()
    {
        base.Release();
        if (!isLaunched)
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

        isLaunched = true;

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
        isLaunched = false;

        StopResetCoroutine();
    }

}
