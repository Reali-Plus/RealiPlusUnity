using System.Text;

public struct HapticsData
{
    // TODO : replace with actual data
    public string Message { get; private set; }

    public HapticsData(string message)
    {
        Message = message;
    }

    public void UpdateData(string message)
    {
        Message = message;
    }

    public byte[] ToBytes()
    {
        // TODO : replace with actual data format
        return Encoding.UTF8.GetBytes(Message);
    }
}   
