using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

public struct RealiPlusHapticsCommand : IInputDeviceCommandInfo
{
    public static FourCC Type { get { return new FourCC('R', 'M', 'B', 'L'); } } // TODO: Decode this line
    private const int size = InputDeviceCommand.BaseCommandSize + sizeof(byte) + sizeof(float) * 2;

    public InputDeviceCommand BaseCommand;
    public byte FingerIndex;
    public float HapticValue;
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
