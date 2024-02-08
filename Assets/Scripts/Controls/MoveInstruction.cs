using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public struct MoveInstruction
{
    [SerializeField] private PositionEvent positionEvent;
    [SerializeField] private float speed;
    [SerializeField] private InputAction action;

    private Coroutine executionCoroutine;

    public void Enable()
    {
        action.performed += SetDirection;
        action.Enable();
    }

    public void Disable()
    {
        action.performed -= SetDirection;
        action.Disable();
    }

    private void SetDirection(InputAction.CallbackContext context)
    {
        if (executionCoroutine != null)
        {
            CoroutineRunner.Stop(executionCoroutine);
            executionCoroutine = null;
        }

        Vector3 direction = context.ReadValue<Vector3>();

        if (direction != Vector3.zero)
        {
            executionCoroutine = CoroutineRunner.Run(Move(direction));
        }
    }

    private IEnumerator Move(Vector3 direction)
    {
        YieldInstruction wait = new WaitForFixedUpdate();

        while (true)
        {
            positionEvent.SetValue(positionEvent.CurrentValue + direction * speed * Time.fixedDeltaTime);
            yield return wait;
        }
    }
}
