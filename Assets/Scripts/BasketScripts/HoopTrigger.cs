using UnityEngine;

public class HoopTrigger : MonoBehaviour
{
    public int score = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            score += 2;
            Debug.Log("Score: " + score);
        }
    }
}
