using System;
using UnityEngine;
using UnityEngine.InputSystem;

public struct RealiPlusHaptics
{
    public readonly byte Size;
    public readonly float[] HapticStrength;
    public readonly float[] PiezoStrength;

    public RealiPlusHaptics(byte size)
    {
        Size = size;
        HapticStrength = new float[Size];
        PiezoStrength = new float[Size];
    }

    public void PauseHaptics(InputDevice device)
    {
        if (device == null)
            throw new ArgumentNullException("device");

        for (byte b = 0; b < Size; b++)
        {
            var command = RealiPlusHapticsCommand.Create(b, 0f, 0f);
            device.ExecuteCommand(ref command);
        }
    }

    public void ResumeHaptics(InputDevice device)
    {
        if (device == null)
            throw new ArgumentNullException("device");

        for (byte b = 0; b < Size; b++)
        {
            SetHaptics(device, b, HapticStrength[b], PiezoStrength[b]);
        }
    }

    public void ResetHaptics(InputDevice device)
    {
        if (device == null)
            throw new ArgumentNullException("device");

        for (byte b = 0; b < Size; b++)
        {
            SetHaptics(device, b, 0f, 0f);
        }
    }

    public void SetHaptics(InputDevice device, byte fingerIndex, float hapticValue, float piezoValue = 0)
    {
        if (device == null)
            throw new ArgumentNullException("device");

        HapticStrength[fingerIndex] = Mathf.Clamp(hapticValue, 0.0f, 1.0f);
        PiezoStrength[fingerIndex] = Mathf.Clamp(piezoValue, 0.0f, 1.0f);

        var command = RealiPlusHapticsCommand.Create(fingerIndex, HapticStrength[fingerIndex], PiezoStrength[fingerIndex]);
        device.ExecuteCommand(ref command);
    }
}
