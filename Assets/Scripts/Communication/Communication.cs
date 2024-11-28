using System.Collections;
using System.Collections.Generic;

public abstract class Communication
{
    protected Queue<SleeveData> rcvDataQueue;
    protected Queue<HapticsData> sendDataQueue;

    public Communication()
    {
        rcvDataQueue = new Queue<SleeveData>();
        sendDataQueue = new Queue<HapticsData>();
    }

    public abstract void Initialize();
    public abstract void OnExit();
    public abstract bool ReceiveData();
    public abstract void SendData(HapticsData hapticsData);
    public abstract bool TestCommunication();
    public abstract void Close();

    protected void AddData(SleeveData sleeveData)
    {
        rcvDataQueue.Enqueue(sleeveData);
    }

    protected bool AddDataToReceive(string data)
    {
        SleeveData sleeveData = new SleeveData();
        if (sleeveData.FromString(data))
        {
            rcvDataQueue.Enqueue(sleeveData);
            return true;
        }
        return false;
    }
    public void AddDataToSend(HapticsData data)
    {
        sendDataQueue.Enqueue(data);
    }

    public SleeveData GetDataToReceive()
    {
        if (HasDataToReceive())
        {
            return rcvDataQueue.Dequeue();
        }
        return new SleeveData();
    }

    public HapticsData GetDataToSend()
    {
        return sendDataQueue.Dequeue();
    }

    public bool HasDataToReceive()
    {
        return rcvDataQueue.Count > 0;
    }

    public bool HasDataToSend()
    {
        return sendDataQueue.Count > 0;
    }
}
