using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public struct JointInstruction
{
    [SerializeField] private NormalizedEvent jointEvent;
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

        float direction = context.ReadValue<float>();

        if (direction != 0f)
        {
            executionCoroutine = CoroutineRunner.Run(Move(direction));
        }
    }

    private IEnumerator Move(float direction)
    {
        YieldInstruction wait = new WaitForFixedUpdate();

        while (true)
        {
            jointEvent.SetValue(jointEvent.CurrentValue + direction * speed * Time.fixedDeltaTime);
            yield return wait;
        }
    }
}
