using System.Collections;
using UnityEngine;

public class JointController : MonoBehaviour
{
    [SerializeField] private JointEvent jointEvent;
    [SerializeField] private float minRotation;
    [SerializeField] private float maxRotation;
    [SerializeField] private float flexTime = 1f;

    private Coroutine flexCoroutine;

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
        Quaternion startRot = transform.localRotation;
        Vector3 targetEuler = new Vector3(targetX, 0f, 0f);
        Quaternion endRot = Quaternion.Euler(targetEuler);

        float elapsedTime = 0f;

        while (elapsedTime < flexTime)
        {
            elapsedTime += Time.deltaTime;
            transform.localRotation = Quaternion.Lerp(startRot, endRot, elapsedTime / flexTime);

            yield return null;
        }

        flexCoroutine = null;
    }
}
