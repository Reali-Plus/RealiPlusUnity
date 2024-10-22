public class HapticsData
{
    public SensorID FingerId { get; private set; }
    public bool RetroactionResponse { get; private set; }
    public bool RestrictionResponse { get; private set; }

    public HapticsData()
    {
        FingerId = SensorID.Logic;
        RetroactionResponse = false;
        RestrictionResponse = false;
    }

    public HapticsData(SensorID sensorID)
    {
        FingerId = sensorID;
        RetroactionResponse = false;
        RestrictionResponse = false;
    }

    public HapticsData(SensorID fingerId, bool retroactionResponse, bool restrictionResponse)
    {
        FingerId = fingerId;
        RetroactionResponse = retroactionResponse;
        RestrictionResponse = restrictionResponse;
    }

    public void UpdateFeedback(bool retroaction, bool restriction)
    {
        RetroactionResponse = retroaction;
        RestrictionResponse = restriction;
    }

    public static string BoolToString(bool value) => value ? "1" : "0";

    public override string ToString()
    {
        return FingerId + BoolToString(RetroactionResponse) + BoolToString(RestrictionResponse);
    }
}