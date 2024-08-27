using System;
using UnityEngine;

public class XRSleeveCommunicationManager
{
    public static XRSleeveCommunicationManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new XRSleeveCommunicationManager();
            }
            return instance;
        }
    }
    private static XRSleeveCommunicationManager instance;

    public XRSleeveDevice LeftDevice { get; private set; }
    public XRSleeveDevice RightDevice { get; private set; }

    public void SetLeftDevice(XRSleeveDevice device)
    {
        if (LeftDevice != null)
        {
            LeftDevice.Side = SleeveSide.Default;
        }

        if (device != null && device == RightDevice)
        {
            SetRightDevice(null);
        }

        LeftDevice = device;

        if (device != null)
        {
            device.Side = SleeveSide.Left;
        }
    }

    public void SetRightDevice(XRSleeveDevice device)
    {
        if (RightDevice != null)
        {
            RightDevice.Side = SleeveSide.Default;
        }

        if (device != null && device == LeftDevice)
        {
            SetLeftDevice(null);
        }

        RightDevice = device;

        if (device != null)
        {
            device.Side = SleeveSide.Right;
        }
    }

    public void ClearConfig()
    {
        if (LeftDevice != null)
        {
            LeftDevice.Side = SleeveSide.Default;
            LeftDevice = null;
        }

        if (RightDevice != null)
        {
            RightDevice.Side = SleeveSide.Default;
            RightDevice = null;
        }
    }

    public XRSleeveDevice[] FetchAvailableDevices() // TODO: Start coroutine
    {
        // TODO: Placeholder
        ClearConfig();
        string[] data = new string[] { "Device_1", "Device_2", "Device_3" };

        XRSleeveDevice[] devices = new XRSleeveDevice[data.Length];

        for (int i = 0; i < data.Length; i++)
        {
            devices[i] = new XRSleeveDevice(data[i]);
        }

        return devices;
    }
}
