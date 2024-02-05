using UnityEngine;

public class JointDispatcher : MonoBehaviour
{
    [SerializeField] private JointInstruction[] joints;

    private void OnEnable()
    {
        foreach (var joint in joints)
        {
            joint.Enable();
        }
    }

    private void OnDisable()
    {
        foreach (var joint in joints)
        {
            joint.Disable();
        }
    }
}
