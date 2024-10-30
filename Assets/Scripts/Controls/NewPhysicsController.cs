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
        rigidBody.MovePosition(target.position);
        rigidBody.MoveRotation(target.rotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, target.position);
    }
}
