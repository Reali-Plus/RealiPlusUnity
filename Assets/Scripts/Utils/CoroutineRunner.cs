using System.Collections;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
    private static CoroutineRunner instance;

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public static Coroutine Run(IEnumerator Coroutine)
    {
        if (instance == null)
        {
            GameObject runner = new GameObject("CoroutineRunner");
            instance = runner.AddComponent<CoroutineRunner>();
        }

        return instance.StartCoroutine(Coroutine);
    }

    public static void Stop(Coroutine coroutine)
    {
        instance?.StopCoroutine(coroutine);
    }
}
