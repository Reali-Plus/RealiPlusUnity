using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RealiPlusDevice : InputDevice, IRealiPlusHaptics
{
    private RealiPlusHaptics haptics;

    public void SetHaptics(byte fingerIndex, float hapticValue, float piezoValue = 0)
    {
        haptics.SetHaptics(this, fingerIndex, hapticValue, piezoValue);
    }

    public void PauseHaptics()
    {
        haptics.PauseHaptics(this);
    }

    public void ResetHaptics()
    {
        haptics.ResetHaptics(this);
    }

    public void ResumeHaptics()
    {
        haptics.ResumeHaptics(this);
    }
}
