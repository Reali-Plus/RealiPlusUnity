using UnityEngine;

public class HandPhysicsLauncher : MonoBehaviour
{
    public Rigidbody handRigidbody;   // Rigidbody of the hand
    public Rigidbody ballRigidbody;   // Rigidbody of the ball
    public Transform hoop;            // Reference to the hoop
    public float swingForce = 500f;   // Force to swing the hand
    public float launchForce = 10f;   // Force to launch the ball
    public float releaseTime = 0.5f;  // Time after which to release the ball

    private bool ballLaunched = false;

    void Start()
    {
        // Apply an initial force to swing the hand
        MoveHand();
        
        // Start coroutine to launch the ball after the hand swings
        StartCoroutine(LaunchBallAfterDelay(releaseTime));
    }

    void MoveHand()
    {
        // Apply a torque or force to move the hand
        handRigidbody.AddForce(Vector3.forward * swingForce, ForceMode.Impulse);
    }

    System.Collections.IEnumerator LaunchBallAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        LaunchBall();
    }

    void LaunchBall()
    {
        if (ballLaunched) return;

        // Calculate the direction from the hand to the hoop
        Vector3 launchDirection = (hoop.position - ballRigidbody.position).normalized;

        // Apply force to the ball in the calculated direction
        ballRigidbody.AddForce(launchDirection * launchForce, ForceMode.Impulse);

        ballLaunched = true;
    }
}
