using System.Collections.Generic;
using System.Text;

public struct FingerFeedback
{
    public int fingerId;
    public bool retroactionResponse;
    public bool restrictionResponse;

    public FingerFeedback(int fingerId, bool retroactionResponse, bool restrictionResponse)
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
}

public class HapticsData
{
    public List<FingerFeedback> FeedbackList { get; private set; }

    public HapticsData()
    {
        FeedbackList = new List<FingerFeedback>();
    }

    public HapticsData(List<FingerFeedback> feedbacks)
    {
        FeedbackList = feedbacks;
    }

    public void AddFeedback(FingerFeedback feedback)
    {
        FeedbackList.Add(feedback);
    }

    /*public void UpdateData(string message)
    {
        Message = message;
    }*/

    /* public readonly byte[] ToBytes()
     {
         // TODO : replace with actual data format
         return Encoding.UTF8.GetBytes(Message);
     }*/

    public override string ToString()
    {
        string data = "";
        for (int i = 0; i < FeedbackList.Count; i++)
        {
            data += FeedbackList[i].ToString() + " ";
        }

        return data;
    }
}   