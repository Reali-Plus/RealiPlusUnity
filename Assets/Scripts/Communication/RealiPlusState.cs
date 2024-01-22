using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

public struct RealiPlusState : IInputStateTypeInfo
{
    public FourCC format { get { return new FourCC('R', 'E', 'A', 'L'); } }

    [InputControl(layout = "Analog", parameters = "clamp=true,clampMin=0,clampMax=1")]
    public float thumb, finger1, finger2, finger3, finger4;
}
