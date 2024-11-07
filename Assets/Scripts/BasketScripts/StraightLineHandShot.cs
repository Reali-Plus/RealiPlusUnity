using UnityEngine;

public class StraightLineMovement : MonoBehaviour
{
    public float speed = 5f;          // Speed of the hand movement
    private float elapsedTime = 0f;
    private bool shouldMove = true;
    private Rigidbody handRigidbody;

    void Start()
    {
        // Get the Rigidbody component
        handRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Check if the hand should still be moving
        if (shouldMove)
        {
            // Calculate movement direction along the local z-axis (forward)
            Vector3 movement = transform.forward * speed * Time.fixedDeltaTime;

            // Move the Rigidbody using the calculated local movement
            handRigidbody.MovePosition(handRigidbody.position + movement);

            // Update the elapsed time
            elapsedTime += Time.fixedDeltaTime;

            // Stop the movement after 1 second
            if (elapsedTime >= 1f)
            {
                shouldMove = false;
            }
        }
    }
}
