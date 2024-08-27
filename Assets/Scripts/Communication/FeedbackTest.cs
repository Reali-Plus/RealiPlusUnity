using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackTest : MonoBehaviour
{
    public void ToggleFeedback(bool enable)
    {
        float command = enable ? 1f : 0f;
        XRSleeveCommunicationManager.Instance.LeftDevice.SendHapticCommand(command, command);
    }
}
