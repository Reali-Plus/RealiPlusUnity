using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicsMoveController : MonoBehaviour
{
    [SerializeField] private Rigidbody target;
    [SerializeField] private float moveSpeed;
    [SerializeField] private InputAction moveAction;

    private Vector3 currentDirection;

    private void OnEnable()
    {
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
        target.MovePosition(target.position + currentDirection * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnMoveAction(InputAction.CallbackContext context)
    {
        currentDirection = context.ReadValue<Vector3>();
    }
}
