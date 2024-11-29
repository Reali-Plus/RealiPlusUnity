using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketBallZone : MonoBehaviour
{

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out GrabbableBall ball))
        {
            if (!ball.IsGrabbed() && !ball.IsLaunched())
            {
                ball.ResetGrabbableBall();
            }
        }
    }
}
