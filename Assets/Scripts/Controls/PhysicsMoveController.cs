using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsMoveController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private InputAction moveAction;

    private Vector3 currentDirection;
    new private Rigidbody rigidbody;

    private void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody>();

        moveAction.performed += OnMoveAction;

        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.performed -= OnMoveAction;

        moveAction.Disable();
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(transform.position + currentDirection * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnMoveAction(InputAction.CallbackContext context)
    {
        currentDirection = context.ReadValue<Vector3>();
    }
}
