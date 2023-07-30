using UnityEngine;

public class XRSleeveConfigMenu : MonoBehaviour
{
    [SerializeField] private XRSleeveListEntry deviceTemplate;

    private XRSleeveListEntry[] activeDevices;
    private XRSleeveListEntry selectedDevice;

    private void OnDestroy()
    {
        ClearList();
    }

    public void RefreshList()
    {
        ClearList();

        XRSleeveDevice[] devices = XRSleeveCommunicationManager.Instance.FetchAvailableDevices();
        activeDevices = new XRSleeveListEntry[devices.Length];

        for (int i = 0; i < devices.Length; i++)
        {
            XRSleeveListEntry newDevice = Instantiate(deviceTemplate, deviceTemplate.transform.parent);
            newDevice.SetDeviceData(devices[i]);
            newDevice.OnEntrySelected += EntrySelectedCallback;
            activeDevices[i] = newDevice;
        }
    }

    private void ClearList()
    {
        if (activeDevices == null)
        {
            return;
        }

        for (int i = 0; i < activeDevices.Length; i++)
        {
            activeDevices[i].OnEntrySelected -= EntrySelectedCallback;
            Destroy(activeDevices[i].gameObject);
        }
        activeDevices = null;
        selectedDevice = null;
    }

    private void EntrySelectedCallback(XRSleeveListEntry deviceEntry)
    {
        selectedDevice = deviceEntry;
    }

    public void SetActiveAsLeft()
    {
        if (selectedDevice == null)
        {
            return;
        }

        XRSleeveCommunicationManager.Instance.SetLeftDevice(selectedDevice.DeviceData);
    }

    public void SetActiveAsRight()
    {
        if (selectedDevice == null)
        {
            return;
        }

        XRSleeveCommunicationManager.Instance.SetRightDevice(selectedDevice.DeviceData);
    }

    public void ClearDeviceConfig()
    {
        XRSleeveCommunicationManager.Instance.ClearConfig();
    }

    public void ExitMenu()
    {
        if (XRSleeveCommunicationManager.Instance.LeftDevice == null ||
            XRSleeveCommunicationManager.Instance.RightDevice == null)
        {
            Debug.Log("Devices must be configured before leaving menu.");
            return;
        }

        GameSceneManager.LoadScene("PianoTestScene_1");
    }
}
