
public struct SleeveData
{
    // TODO : Add other data
    public float Roll { get; private set; }
    public float Pitch { get; private set; }
    public float Yaw { get; private set; }

    public SleeveData(float roll, float pitch, float yaw)
    {
        Roll = roll;
        Pitch = pitch;
        Yaw = yaw;
    }

    public void UpdateData(float roll, float pitch, float yaw)
    {
        Roll = roll;
        Pitch = pitch;
        Yaw = yaw;
    }

    public void FromString(string message)
    {
        string[] data = message.Split(" ");
        
        if (data.Length >= 3)
        {
            Roll = ParseRotation(data[0]);
            Pitch = ParseRotation(data[1]);
            Yaw = ParseRotation(data[2]);
        }
    }

    private readonly float ParseRotation(string strRotation)
    {
        return float.TryParse(strRotation, out float rotation) ? rotation : 0f;
    }
}