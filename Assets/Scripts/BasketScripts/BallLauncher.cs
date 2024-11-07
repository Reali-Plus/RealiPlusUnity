using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    public Rigidbody ballRigidbody; // Reference to the Rigidbody of the ball
    public float launchForce = 10f; // Force to launch the ball
    public Transform hand;          // Reference to the hand position, used to calculate direction

    void Start()
    {
        // Apply force to the ball at the start of the scene
        LaunchBall();
    }

    void LaunchBall()
    {
        if (ballRigidbody != null && hand != null)
        {
            // Calculate direction from hand to the target direction (e.g., hoop)
            Vector3 launchDirection = (hand.forward).normalized;

            // Apply force to the ball in the calculated direction
            ballRigidbody.AddForce(launchDirection * launchForce, ForceMode.Impulse);

            // Optionally, log the launch to the console for verification
            Debug.Log("Ball launched with force: " + launchForce);
        }
    }
}
