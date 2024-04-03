using UnityEngine;

public class RealiPlusDispatcher : MonoBehaviour
{
    [SerializeField] private JointToggleInstruction[] joints;
    [SerializeField] private JointInstruction[] smoothJoints;
    [SerializeField] private MoveInstruction[] movements;

    private void OnEnable()
    {
        foreach (var joint in joints)
        {
            joint.Enable();
        }

        foreach (var movement in movements)
        {
            movement.Enable();
        }

        foreach (var smoothJoint in smoothJoints)
        {
            smoothJoint.Enable();
        }
    }

    private void OnDisable()
    {
        foreach (var joint in joints)
        {
            joint.Disable();
        }

        foreach (var movement in movements)
        {
            movement.Disable();
        }

        foreach (var smoothJoint in smoothJoints)
        {
            smoothJoint.Disable();
        }
    }
}
