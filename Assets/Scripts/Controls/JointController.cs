using System.Collections;
using UnityEngine;

public class JointController : MonoBehaviour
{
    [SerializeField] private JointEvent jointEvent;
    [SerializeField] private float minRotation;
    [SerializeField] private float maxRotation;
    [SerializeField, Min(5f)] private float flexSpeed = 20f;

    //private Vector3 baseLocalEulers;
    private Coroutine flexCoroutine;
    private float currentX = 0f;

    /*
    private void Awake()
    {
        baseLocalEulers = transform.localEulerAngles;
    }*/

    private void OnEnable()
    {
        jointEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        jointEvent.UnregisterListener(this);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();

        flexCoroutine = null;
    }

    public void UpdateValue(float value)
    {
        if (flexCoroutine != null)
        {
            StopCoroutine(flexCoroutine);
        }

        flexCoroutine = StartCoroutine(FlexCoroutine(Mathf.Lerp(minRotation, maxRotation, value)));
    }

    private IEnumerator FlexCoroutine(float targetX)
    {
        bool inProgress = true;
        while (inProgress)
        {
            float angleStep = (targetX > currentX ? flexSpeed : -flexSpeed) * Time.deltaTime;

            if (angleStep >= 0 && currentX + angleStep >= targetX ||
                angleStep < 0 && currentX + angleStep <= targetX)
            {
                angleStep = targetX - currentX;
                inProgress = false;
            }
            currentX += angleStep;

            transform.Rotate(Vector3.right, angleStep, Space.Self);
            yield return null;
        }

        flexCoroutine = null;
    }
}
