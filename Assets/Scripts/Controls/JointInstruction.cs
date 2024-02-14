using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public struct JointInstruction
{    
    [SerializeField] private NormalizedEvent jointEvent;
    [SerializeField] private AnimationCurve transitionCurve;
    [SerializeField] private InputAction action;

    private bool toggled;
    private Coroutine executionCoroutine;

    public void Enable()
    {
        action.performed += Toggle;
        action.Enable();
    }

    public void Disable()
    {
        action.performed -= Toggle;
        action.Disable();
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
        float endTime = transitionCurve.keys[transitionCurve.keys.Length - 1].time;

        float startVal = jointEvent.CurrentValue;

        while (currentTime < endTime)
        {
            float curveVal = transitionCurve.Evaluate(currentTime);
            jointEvent.SetValue( (1 - curveVal) * startVal + curveVal * targetVal );

            yield return wait;
            currentTime += Time.fixedDeltaTime;
        }

        jointEvent.SetValue(targetVal);
        executionCoroutine = null;
    }
}
