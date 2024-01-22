using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Haptics;

public interface IRealiPlusHaptics : IHaptics
{
    public void SetHaptics(byte fingerIndex, float hapticValue, float piezoValue = 0f);
}
