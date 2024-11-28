using UnityEngine;

public class BallLauncher : MiniGameManager
{
    public Rigidbody ballRigidbody;
    public float launchForce = 10f;
    public Transform hoop;
    public Transform initialPosition;
    public Collider hoopCollider;
    private bool ballLaunched = false;

    public override void Initialize() {  }

    protected override void StartGame()
    {
        ballRigidbody.useGravity = false;
        ballRigidbody.position = initialPosition.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !ballLaunched)
        {
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

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(ResetBall());
    }

    System.Collections.IEnumerator ResetBall()
    {
        yield return new WaitForSeconds(0.5f);
        ResetBasket();
    }

    private void ResetBasket()
    {
        ballRigidbody.velocity = Vector3.zero;
        ballRigidbody.angularVelocity = Vector3.zero;
        ballRigidbody.useGravity = false;
        ballRigidbody.position = initialPosition.position;
        ballLaunched = false;
    }

    protected override void ResetGame()
    {
        ResetBasket();
    }
}
