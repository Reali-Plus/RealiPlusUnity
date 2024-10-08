using UnityEngine;
using Random = UnityEngine.Random;

public class TransformNoise : MonoBehaviour
{
    [SerializeField] private Vector3 drift = Vector3.zero;
    [SerializeField] private float positionNoise = 0.002f;
    [SerializeField] private float rotationNoise = 0.5f;

    private void Update()
    {
        transform.position += (new Vector3(Random.Range(-positionNoise, positionNoise),
                                          Random.Range(-positionNoise, positionNoise),
                                          Random.Range(-positionNoise, positionNoise)) +
                               drift);

        transform.Rotate(Random.onUnitSphere, Random.value * rotationNoise);
    }
}
