using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public struct JointInstruction
{
    public JointEvent Event;
    public AnimationCurve Curve;
    public InputAction Action;

    private bool toggled;
    private Coroutine executionCoroutine;

    public void Enable()
    {
        Action.performed += Toggle;
        Action.Enable();
    }

    public void Disable()
    {
        Action.performed -= Toggle;
        Action.Disable();
    }

    private void Toggle(InputAction.CallbackContext context)
    {
        toggled = !toggled;

        if (executionCoroutine != null)
        {
            CoroutineRunner.Stop(executionCoroutine);
        }
        executionCoroutine = CoroutineRunner.Run(LerpValue(toggled ? 1f : 0f));
    }

    private IEnumerator LerpValue(float targetVal)
    {
        YieldInstruction wait = new WaitForFixedUpdate();
        float currentTime = 0;
        float endTime = Curve.keys[Curve.keys.Length - 1].time;

        float startVal = Event.CurrentValue;

        while (currentTime < endTime)
        {
            float curveVal = Curve.Evaluate(currentTime);
            Event.SetValue( (1 - curveVal) * startVal + curveVal * targetVal );

            yield return wait;
            currentTime += Time.fixedDeltaTime;
        }

        Event.SetValue(targetVal);
        executionCoroutine = null;
    }
}
