using UnityEngine;

public class RealiPlusDispatcher : MonoBehaviour
{
    [SerializeField] private JointInstruction[] joints;
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
    }
}
