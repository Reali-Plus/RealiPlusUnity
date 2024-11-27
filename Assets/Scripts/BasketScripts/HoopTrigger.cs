using UnityEngine;

public class HoopTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem goalVFX;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball") && goalVFX != null)
        {
            goalVFX.Play();
        }
    }
}
