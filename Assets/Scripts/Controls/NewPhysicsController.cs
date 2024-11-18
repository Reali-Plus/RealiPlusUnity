using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NewPhysicsController : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Rigidbody rigidBody;

    private void OnEnable()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        // rigidBody.MovePosition(target.position);
        rigidBody.MoveRotation(target.rotation);
        
        rigidBody.velocity = (target.position - rigidBody.position) / Time.fixedDeltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, target.position);
    }
}
