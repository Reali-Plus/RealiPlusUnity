using System.Runtime.InteropServices;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

[StructLayout(LayoutKind.Explicit, Size = size)]
public struct RealiPlusHapticsCommand : IInputDeviceCommandInfo
{
    public static FourCC Type { get { return new FourCC('R', 'E', 'A', 'L'); } }
    private const int size = InputDeviceCommand.BaseCommandSize + sizeof(byte) + 2 * sizeof(float);

    [FieldOffset(0)]
    public InputDeviceCommand BaseCommand;

    [FieldOffset(InputDeviceCommand.BaseCommandSize)]
    public byte FingerIndex;

    [FieldOffset(InputDeviceCommand.BaseCommandSize + sizeof(byte))]
    public float HapticValue;

    [FieldOffset(InputDeviceCommand.BaseCommandSize + sizeof(byte) + sizeof(float))]
    public float PiezoValue;

    public FourCC typeStatic
    {
        get { return Type; }
    }

    public static RealiPlusHapticsCommand Create(byte fingerIndex, float hapticValue, float piezoValue)
    {
        return new RealiPlusHapticsCommand
        {
            BaseCommand = new InputDeviceCommand(Type, size),
            HapticValue = hapticValue,
            PiezoValue = piezoValue
        };
    }
}
