using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrajectory : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField, Min(10)]
    private int lineSegments = 50;

    [SerializeField, Min(1)]
    private float timeOfFlight = 5f;

    public void ShowTrajectoryLine(Vector3 initialPosition, Vector3 initialVelocity)
    {
        Vector3[] linePoints = CalculateTrajectoryLine(initialPosition, initialVelocity);
        
        lineRenderer.SetPositions(linePoints);
        lineRenderer.positionCount = lineSegments;
    }

    public Vector3[] CalculateTrajectoryLine(Vector3 initialPosition, Vector3 initialVelocity)
    {

       float timeStep = timeOfFlight / lineSegments;

        Vector3[] linePoints = new Vector3[lineSegments];

        for (int i = 0; i < lineSegments; ++i)
        {
            float timeOffset = i * timeStep;

            Vector3 progressBeforeGravity = initialVelocity * timeOffset;
            Vector3 gravityOffset = Physics.gravity * timeOffset * timeOffset / 2f;

            linePoints[i] = initialPosition + progressBeforeGravity + gravityOffset;
        }

        return linePoints;
    }
}
