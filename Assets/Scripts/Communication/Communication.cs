using System.Collections.Generic;

public abstract class Communication
{
    protected Queue<SleeveData> dataQueue;

    public Communication()
    {
        dataQueue = new Queue<SleeveData>();
    }

    public abstract void Initialize();
    public abstract void OnExit();
    public abstract bool ReceiveData();
    public abstract void SendData(HapticsData hapticsData);
    public abstract bool TestCommunication();
    public abstract void Close();

    protected void AddData(SleeveData sleeveData)
    {
        dataQueue.Enqueue(sleeveData);
    }

    protected bool AddData(string data)
    {
        SleeveData sleeveData = new SleeveData();
        if (sleeveData.FromString(data))
        {
            dataQueue.Enqueue(sleeveData);
            return true;
        }
        return false;
    }

    public SleeveData GetData()
    {
        if (HasData())
        {
            return dataQueue.Dequeue();
        }
        return new SleeveData();
    }

    public bool HasData()
    {
        return dataQueue.Count > 0;
    }
}
