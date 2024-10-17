using System.Collections.Generic;

/*public struct FingerFeedback
{
    public SensorID fingerId;
    public bool retroactionResponse;
    public bool restrictionResponse;

    public FingerFeedback(SensorID fingerId, bool retroactionResponse, bool restrictionResponse)
    {
        this.fingerId = fingerId;
        this.retroactionResponse = retroactionResponse;
        this.restrictionResponse = restrictionResponse;
    }

    public static string BoolToString(bool value) => value ? "1" : "0";

    public override string ToString()
    {
        return fingerId + BoolToString(retroactionResponse) + BoolToString(restrictionResponse);
    }
}*/

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

    // public List<FingerFeedback> FeedbackList { get; private set; }

    /*public HapticsData()
    {
        FeedbackList = new List<FingerFeedback>();
    }

    public HapticsData(List<FingerFeedback> feedbacks)
    {
        FeedbackList = feedbacks;
    }

    public HapticsData(SensorID fingerId, bool retroactionResponse, bool restrictionResponse)
    {
        FeedbackList = new List<FingerFeedback>
        {
            new FingerFeedback(fingerId, retroactionResponse, restrictionResponse)
        };
    }

    public void AddFeedback(FingerFeedback feedback)
    {
        FeedbackList.Add(feedback);
    }

    public void ClearFeedback()
    {
        FeedbackList.Clear();
    }

    public override string ToString()
    {
        string data = "";
        for (int i = 0; i < FeedbackList.Count; i++)
        {
            data += FeedbackList[i].ToString() + " ";
        }

        return data;
    }*/
}