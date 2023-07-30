using UnityEngine;
using System;
using TMPro;

public class XRSleeveListEntry : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameDisplay;
    [SerializeField] private TextMeshProUGUI labelDisplay;

    public XRSleeveDevice DeviceData { get; private set; }

    public event Action<XRSleeveListEntry> OnEntrySelected;

    public void SetDeviceData(XRSleeveDevice device)
    {
        if (DeviceData != null)
        {
            DeviceData.OnConfigChanged -= ConfigChangedCallback;
        }

        DeviceData = device;
        DeviceData.OnConfigChanged += ConfigChangedCallback;
        nameDisplay.text = device.DeviceName;
        gameObject.SetActive(true);
    }

    public void ConfigChangedCallback(SleeveSide newSide)
    {
        switch (newSide)
        {
            case SleeveSide.Left:
                {
                    labelDisplay.text = "L";
                    break;
                }

            case SleeveSide.Right:
                {
                    labelDisplay.text = "R";
                    break;
                }

            default:
                {
                    labelDisplay.text = String.Empty;
                    break;
                }
        }
    }

    public void SetActiveEntry(bool active)
    {
        if (active)
        {
            OnEntrySelected?.Invoke(this);
        }
    }
}
