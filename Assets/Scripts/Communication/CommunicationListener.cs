using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Drawing;
using System.Threading.Tasks;
using System;

public class CommunicationListener : MonoBehaviour
{
    private float roll = 0f;
    private float pitch = 0f;
    private float yaw = 0f;

    private System.Diagnostics.Process daemonProcess;
    private Func<bool> processFunction;

    void Start()
    {
        DirectoryInfo dir = Directory.GetParent(Application.dataPath);
        string daemonPath = Path.Combine(dir.FullName, "Assets", "Scripts", "Communication", "communication-daemon.py");

        daemonProcess = new System.Diagnostics.Process();

        daemonProcess.StartInfo.FileName = "python";
        daemonProcess.StartInfo.Arguments = daemonPath;
        daemonProcess.StartInfo.RedirectStandardOutput = true;
        daemonProcess.StartInfo.UseShellExecute = false;
        daemonProcess.StartInfo.CreateNoWindow = true;

        processFunction = new Func<bool>(() => daemonProcess.Start());

        ProcessScriptOutput();
    }

    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(pitch, yaw, roll);
    }

    async void ProcessScriptOutput()
    {
        bool started = await Task.Run(processFunction);
        Debug.Log($"Process started : {started}");

        string message;
        do
        {
            message = await daemonProcess.StandardOutput.ReadLineAsync();
            if (message != null)
            {
                ConvertMessage(message);
                await Task.Delay(10);
            }
        } while (message != null);
    }

    void ConvertMessage(string message)
    {
        string[] rotations = message.Split(" ");
        
        if (rotations.Length > 1)
        {
            roll = ParseRotation(rotations[0]);
            pitch = ParseRotation(rotations[1]);
            yaw = ParseRotation(rotations[2]);

            Debug.Log("roll " + roll);
            Debug.Log("pitch " + pitch);
            Debug.Log("yaw " + yaw);
        }
    }

    float ParseRotation(string strRotation)
    {
        return float.TryParse(strRotation, out float rotation) ? rotation * Mathf.Rad2Deg : 0f;
    }
}
