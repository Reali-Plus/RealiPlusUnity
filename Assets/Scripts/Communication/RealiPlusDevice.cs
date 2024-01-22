using UnityEditor;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

[InputControlLayout(stateType = typeof(RealiPlusState))]
#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class RealiPlusDevice : InputDevice, IRealiPlusHaptics
{
    static RealiPlusDevice()
    {
        InputSystem.RegisterLayout<RealiPlusDevice>();
    }

    public AxisControl thumb { get; private set; }
    public AxisControl finger1 { get; private set; }
    public AxisControl finger2 { get; private set; }
    public AxisControl finger3 { get; private set; }
    public AxisControl finger4 { get; private set; }

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
    
    protected override void FinishSetup()
    {
        thumb = GetChildControl<AxisControl>("thumb");
        finger1 = GetChildControl<AxisControl>("finger1");
        finger2 = GetChildControl<AxisControl>("finger2");
        finger3 = GetChildControl<AxisControl>("finger3");
        finger4 = GetChildControl<AxisControl>("finger4");

        base.FinishSetup();
    }
}

// TODO: InputSystem.RegisterControlLayout("RealiPlus", typeof(RealiPlusDevice));
