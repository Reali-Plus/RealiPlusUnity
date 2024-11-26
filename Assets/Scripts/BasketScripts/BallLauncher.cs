using UnityEngine;

public class BallLauncher : MiniGameManager
{
    public Rigidbody ballRigidbody;
    public float launchForce = 10f;
    public Transform hoop;
    public Transform initialPosition;
    public Collider hoopCollider;
    private bool ballLaunched = false;
    private float lastLaunchTime = 0f;

    private const float MIN_TIME_SCALE = 0.15f;
    private const float MAX_TIME_SCALE = 0.8f;
    private const float TIME_SCALE_SPEED = 1.5f;
    private const float MAX_LAUNCH_TIME = 6f;

    protected override void StartGame()
    {
        ballRigidbody.useGravity = false;
        ballRigidbody.position = initialPosition.position;
    }

    void Update()
    {
        if (ballLaunched && Time.time > lastLaunchTime + MAX_LAUNCH_TIME)
        {
            ResetBasket();
        }

        if (Input.GetMouseButtonDown(0) && !ballLaunched)
        {
            LaunchBall();
        }

        float timeSpeed = (Input.GetKey(KeyCode.Space) ? -1f : 1f) * TIME_SCALE_SPEED;

        Time.timeScale = Mathf.Clamp(Time.timeScale + timeSpeed * Time.unscaledDeltaTime,
                                     MIN_TIME_SCALE, MAX_TIME_SCALE);
    }

    void LaunchBall()
    {
        if (ballRigidbody != null && hoop != null)
        {
            ballRigidbody.useGravity = true;

            Vector3 launchDirection = (hoop.position - ballRigidbody.position).normalized;

            ballRigidbody.AddForce(launchDirection * launchForce, ForceMode.Impulse);

            ballLaunched = true;
            lastLaunchTime = Time.time;
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
