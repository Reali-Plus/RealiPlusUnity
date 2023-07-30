using UnityEngine;
using UnityEngine.Events;

public class ToggleLever : MonoBehaviour
{
    [SerializeField, Range(0f, 360f)]
    private float targetAngle;

    [SerializeField, Range(0f, 180f)]
    private float targetWidth;

    public UnityEvent<bool> OnStateChanged;

    private bool isToggled;

    private void Update()
    {
        bool inRange = Mathf.DeltaAngle(transform.eulerAngles.x, targetAngle) < targetWidth;

        if (inRange == isToggled)
        {
            return;
        }

        OnStateChanged?.Invoke(inRange);
        isToggled = inRange;
    }
}
