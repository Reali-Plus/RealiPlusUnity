using System.Collections.Generic;
using System.Text;

public struct HapticsData
{
    // TODO : replace with actual data
    public string Message { get; private set; }
    public List<FingerFeedback> FeedbackList { get; private set; }

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
    }

    public HapticsData(string message, List<FingerFeedback> feedbacks)
    {
        Message = message;
        FeedbackList = feedbacks;
    }

    public void UpdateData(string message)
    {
        Message = message;
    }

    public readonly byte[] ToBytes()
    {
        // TODO : replace with actual data format
        return Encoding.UTF8.GetBytes(Message);
    }
}   