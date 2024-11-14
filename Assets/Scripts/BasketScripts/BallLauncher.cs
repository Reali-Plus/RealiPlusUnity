using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    public Rigidbody ballRigidbody;
    public float launchForce = 10f;
    public Transform hoop;
    public Transform initialPosition;
    public Collider hoopCollider;
    private bool ballLaunched = false;


    void Start()
    {
        ballRigidbody.useGravity = false;
        ballRigidbody.position = initialPosition.position;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !ballLaunched)
        {
            Time.timeScale = 0.5f;
            LaunchBall();
        }
    }

    void LaunchBall()
    {
        if (ballRigidbody != null && hoop != null)
        {
            ballRigidbody.useGravity = true;

            Vector3 launchDirection = (hoop.position - ballRigidbody.position).normalized;

            ballRigidbody.AddForce(launchDirection * launchForce, ForceMode.Impulse);

            ballLaunched = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other == hoopCollider)
        {
            StartCoroutine(ResetBall());
        }
    }

    System.Collections.IEnumerator ResetBall()
    {
        yield return new WaitForSeconds(2);
        ballRigidbody.velocity = Vector3.zero;
        ballRigidbody.angularVelocity = Vector3.zero;
        ballRigidbody.useGravity = false;
        ballRigidbody.position = initialPosition.position;
        ballLaunched = false;
    }
}
